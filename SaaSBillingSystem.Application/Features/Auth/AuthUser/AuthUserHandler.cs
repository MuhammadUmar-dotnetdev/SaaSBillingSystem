using MediatR;
using SaaSBillingSystem.Application.Features.Auth.LoginUser;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Auth.AuthUser
{
    public class AuthUserHandler: IRequestHandler<AuthUserCommand, Result<AuthUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        private readonly IOrganizationMembershipRepository _organizationMembershipRepository;

        private readonly IJwtService _jwtService;

        private readonly IPasswordHasher _passwordHasher;
        public AuthUserHandler(
            IUserRepository userRepository,
            IOrganizationMembershipRepository organizationMembershipRepository,
            IJwtService jwtService,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _organizationMembershipRepository = organizationMembershipRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<AuthUserResponse>> Handle(AuthUserCommand command, CancellationToken cancellationToken)
        {
            var user =
            await _userRepository.GetByEmailAsync(command.Email);

            if (user == null)
            {
                return Result<AuthUserResponse>.Failure("Invalid credentials");
            }

            if (!_passwordHasher.Verify(command.Password, user.PasswordHash))
                return Result<AuthUserResponse>.Failure("Invalid credentials");

            var membership = await _organizationMembershipRepository.GetAsync(user.Id, command.OrganizationId);

            if (membership == null)
            {
                return Result<AuthUserResponse>.Failure("Membership not found");
            }

            var authContext = new AuthContext
            {
                UserId = user.Id,
                Email = user.Email,
                OrganizationId = membership.OrganizationId,
                OrganizationName = membership.Organization.Name,
                Role = membership.Role
            };

            var token = _jwtService.GenerateToken(authContext);

            return Result<AuthUserResponse>.Success(new AuthUserResponse
            {
                AccessToken = token.AccessToken,
                ExpiresAtUtc = token.ExpiresAtUtc,
                UserId = user.Id,
                OrganizationId = membership.OrganizationId,
                Role = membership.Role.ToString(),
            });
        }
    }
}
