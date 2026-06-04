using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invitations.AddInvitation
{
    public class CreateInvitationHandler: IRequestHandler<CreateInvitationCommand, Result<CreateInvitationResponse>>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        public CreateInvitationHandler(IInvitationRepository invitationRepository, IOrganizationRepository organizationRepository, IUserRepository userRepository)
        {
            _invitationRepository = invitationRepository;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<CreateInvitationResponse>> Handle(CreateInvitationCommand command, CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.GetByIdAsync(command.OrganizationId);

            if(organization == null)
            {
                return Result<CreateInvitationResponse>.Failure($"Organization with id {command.OrganizationId} was not found");
            }

            var user = await _userRepository.GetByEmailAsync(command.Email);
            if(user == null)
            {
                return Result<CreateInvitationResponse>.Failure($"User with email {command.Email} was not found");
            }

            var invitation = new Invitation(command.OrganizationId, command.Email, command.Role);
            await _invitationRepository.AddAsync(invitation);

            var response = new CreateInvitationResponse
            {
                InvitationId = invitation.Id,
                Email = invitation.Email,
                Token = invitation.Token,
                Role = invitation.Role,
                ExpiresAtUtc = invitation.ExpiresAtUtc
            };
            return Result<CreateInvitationResponse>.Success(response);
        }
    }
}
