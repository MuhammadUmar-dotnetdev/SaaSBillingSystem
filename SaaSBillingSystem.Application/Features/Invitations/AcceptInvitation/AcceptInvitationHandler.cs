using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invitations.AcceptInvitation
{
    public class AcceptInvitationHandler: IRequestHandler<AcceptInvitationCommand, Result<AcceptInvitationResponse>>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizationMembershipRepository _organizationMembershipRepository;
        private readonly IOrganizationRepository _organizationRepository;
        public AcceptInvitationHandler(IInvitationRepository invitationRepository, IUserRepository userRepository, IOrganizationMembershipRepository organizationMembershipRepository, IOrganizationRepository organizationRepository)
        {
            _invitationRepository = invitationRepository;
            _userRepository = userRepository;
            _organizationMembershipRepository = organizationMembershipRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<Result<AcceptInvitationResponse>> Handle(AcceptInvitationCommand command, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetByTokenAsync(command.Token);
            if(invitation == null)
            {
                return Result<AcceptInvitationResponse>.Failure($"Invitation with {command.Token} was not found");
            }
            if(invitation.Status == InvitationStatus.Revoked)
            {
                return Result<AcceptInvitationResponse>.Failure($"Invitation with token {command.Token} is revoked");
            }

            if(invitation.ExpiresAtUtc < DateTime.UtcNow)
            {
                return Result<AcceptInvitationResponse>.Failure($"Invitation with token {command.Token} is expired");
            }

            if(invitation.Status == InvitationStatus.Accepted)
            {
                return Result<AcceptInvitationResponse>.Failure($"Invitation with {command.Token} already accepted before");
            }

            var user = await _userRepository.GetByEmailAsync(invitation.Email);
            
            if(user == null)
            {
                return Result<AcceptInvitationResponse>.Failure("No registered user found for this invitation");
            }

            if(await _organizationMembershipRepository.ExistsAsync(user.Id, invitation.OrganizationId))
            {
                return Result<AcceptInvitationResponse>.Failure("User is already a member of this organization");
            }

            var organization = await _organizationRepository.GetByIdAsync(invitation.OrganizationId);
            if(organization == null)
            {
                return Result<AcceptInvitationResponse>.Failure("Organization in this invitation does not exists");
            }

            var membership = new OrganizationMembership(user.Id, invitation.OrganizationId, invitation.Role);
            await _organizationMembershipRepository.AddAsync(membership);
            invitation.Accept();
            await _invitationRepository.UpdateAsync(invitation);


            var response = new AcceptInvitationResponse()
            {
                OrganizationId = invitation.OrganizationId,
                OrganizationName = organization.Name,
                Role = invitation.Role,
            };

            return Result<AcceptInvitationResponse>.Success(response);
        }
    }
}
