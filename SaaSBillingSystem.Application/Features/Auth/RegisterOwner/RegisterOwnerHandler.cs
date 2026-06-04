using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Auth.RegisterOwner
{
    public class RegisterOwnerHandler: IRequestHandler<RegisterOwnerCommand, Result<RegisterOwnerResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IOrganizationMembershipRepository _organizationMembershipRepository;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterOwnerHandler(IUserRepository userRepository, IOrganizationRepository organizationRepository, IPasswordHasher passwordHasher, IOrganizationMembershipRepository organizationMembershipRepository)
        {
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
            _passwordHasher = passwordHasher;
            _organizationMembershipRepository = organizationMembershipRepository;
        }
        public async Task<Result<RegisterOwnerResponse>> Handle(RegisterOwnerCommand command, CancellationToken cancellationToken)
        {
            var userExists = await _userRepository.ExistsByNameAsync(command.Email);
            if(userExists)
            {
                return Result<RegisterOwnerResponse>.Failure("User already exists");
            }
            var organizationExists = await _organizationRepository.ExistsByNameAsync(command.OrganizationName);
            if(organizationExists)
            {
                return Result<RegisterOwnerResponse>.Failure("Organization already exists");
            }
            var passwordHash = _passwordHasher.Hash(command.Password);
            var newUser = new User(command.Email, passwordHash);
            await _userRepository.AddAsync(newUser);
            var newOrganization = new Organization(command.OrganizationName);
            await _organizationRepository.AddAsync(newOrganization);
            var newMembership = new OrganizationMembership(newUser.Id, newOrganization.Id, OrganizationRole.Owner);
            await _organizationMembershipRepository.AddAsync(newMembership);
            return Result<RegisterOwnerResponse>.Success(new RegisterOwnerResponse
            {
                UserId = newUser.Id,
                OrganizationId = newOrganization.Id,
            });
        }
    }
}
