using MediatR;
using SaaSBillingSystem.Application.Features.Auth.AuthDTOs;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;
using System.Security.Cryptography;

namespace SaaSBillingSystem.Application.Features.Auth.LoginUser;

public class LoginUserHandler: IRequestHandler<LoginUserCommand, Result<LoginUserResponse>>
{
    private readonly IUserRepository _userRepository;

    private readonly IOrganizationMembershipRepository _organizationMembershipRepository;

    private readonly IOrganizationRepository _organizationRepository;

    private readonly IPasswordHasher _passwordHasher;

    private readonly ICacheService _cacheService;
    public LoginUserHandler(
        IUserRepository userRepository,
        IOrganizationMembershipRepository organizationMembershipRepository,
        IOrganizationRepository organizationRepository,
        IPasswordHasher passwordHasher,
        ICacheService cacheService)
    {
        _userRepository = userRepository;
        _organizationMembershipRepository = organizationMembershipRepository;
        _organizationRepository = organizationRepository;
        _passwordHasher = passwordHasher;
        _cacheService = cacheService;
    }

    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user =
            await _userRepository.GetByEmailAsync(command.Email);

        if (user == null)
        {
            return Result<LoginUserResponse>.Failure("Invalid credentials");
        }

        if (!_passwordHasher.Verify(command.Password, user.PasswordHash))
        {
            return Result<LoginUserResponse>.Failure("Invalid credentials");
        }
            

        var memberships = await _organizationMembershipRepository.GetByUserIdAsync(user.Id);

        if (memberships.Count == 0)
        {
            return Result<LoginUserResponse>.Failure("No membership found for this user");
        }

        var membershipRoles = memberships.ToDictionary(m => m.OrganizationId, m => m.Role);
        var organizationIds = memberships.Select(m => m.OrganizationId).ToList();

        var organizations = await _organizationRepository.GetByIdsAsync(organizationIds);

        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

        await _cacheService.SetAsync(
            CacheKeys.LoginSession(token),
            new LoginSession
            {
                UserId = user.Id,
                Email= user.Email,
                CreatedAtUtc = DateTime.UtcNow
            },
            TimeSpan.FromMinutes(5));

        var responseOrganizations = organizations.Select(o => new OrganizationSummaryDto
        {
            OrganizationId = o.Id,
            OrganizationName = o.Name,
            Role = membershipRoles[o.Id]
        }).ToList();

        var response = new LoginUserResponse
        {
            LoginToken = token,
            Organizations = responseOrganizations
        };

        return Result<LoginUserResponse>.Success(response);
    }
}