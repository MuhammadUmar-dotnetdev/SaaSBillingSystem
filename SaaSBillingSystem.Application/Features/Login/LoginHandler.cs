using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Login
{
    public class LoginHandler: IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IPasswordHasher _passwordHasher;

        public LoginHandler(IUserRepository userRepository, IOrganizationRepository organizationRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _organizationRepository = organizationRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<LoginResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if(user == null)
            {
                return Result<LoginResponse>.Failure("User not found");
            }

            if(!_passwordHasher.Verify(command.Password, user.PasswordHash))
            {
                return Result<LoginResponse>.Failure("Invalid email or password");
            }

            var memberships = await _userRepository.GetOrganizationMembershipsAsync(user.Id);
            if(memberships.Count == 0)
            {
                return Result<LoginResponse>.Failure("User is not member of any organization");
            }
            var orgIds = memberships.Select(m => m.OrganizationId).ToList();
            var organizations = await _organizationRepository.GetByIdsAsync(orgIds);
            var orgDict = organizations.ToDictionary(o => o.Id, o => o.Name);

            var userOrganizations = memberships.Select(m => new UserOrganizationDTO
            {
                OrganizationId = m.OrganizationId,
                OrganizationName = orgDict[m.OrganizationId],
                organizationRole = m.Role
            }).ToList();

            var response = new LoginResponse
            {
                Id = user.Id,
                Email = user.Email,
                UserOrganizations = userOrganizations
            };
            return Result<LoginResponse>.Success(response);
        }
    }
}
