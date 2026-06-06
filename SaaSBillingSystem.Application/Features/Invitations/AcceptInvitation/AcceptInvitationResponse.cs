using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Application.Features.Invitations.AcceptInvitation
{
    public class AcceptInvitationResponse
    {
        public Guid OrganizationId { get; init; }

        public string OrganizationName { get; init; } = string.Empty;

        public OrganizationRole Role { get; init; }
    }
}
