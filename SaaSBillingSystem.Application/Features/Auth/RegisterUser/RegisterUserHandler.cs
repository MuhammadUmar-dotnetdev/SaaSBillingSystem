using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Auth.RegisterUser;

public class RegisterUserHandler
    : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly ICacheService _cacheService;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IOrganizationRepository organizationRepository, ICacheService cacheService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _organizationRepository = organizationRepository;
        _cacheService = cacheService;
    }

    public async Task<RegisterUserResponse> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new Exception("Email is required");

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new Exception("Password is required");

        var existingUser =
            await _userRepository.GetByEmailAsync(request.Email);

        if (existingUser != null)
            throw new Exception("User already exists");

        var hashedPaswword = _passwordHasher.Hash(request.Password);

        var orgExists = await _organizationRepository.ExistsByNameAsync(request.OrganizationName);
        if(orgExists)
        {
            //return Result<bool>.Failure("Organization Already Exists");
            //return default!;
            throw new Exception("Organization already exists");
        }
        var organization = new Organization(request.OrganizationName);
        await _organizationRepository.AddAsync(organization);
        var user = new User(request.Email, hashedPaswword, organization.Id);

        await _userRepository.AddAsync(user);

        var keyToRemove = $"user:{user.Id}";
        await _cacheService.RemoveAsync(keyToRemove);

        return new RegisterUserResponse
        {
            UserId = user.Id,
            Email = user.Email
        };
    }
}