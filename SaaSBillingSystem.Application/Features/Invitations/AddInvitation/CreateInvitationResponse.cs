using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Application.Features.Invitations.AddInvitation
{
    public class CreateInvitationResponse
    {
        public Guid InvitationId { get; init; }

        public string Email { get; init; } = string.Empty;
        public string Token {  get; init; } = string.Empty;
        public OrganizationRole Role { get; init; }

        public DateTime ExpiresAtUtc { get; init; }
    }
}
