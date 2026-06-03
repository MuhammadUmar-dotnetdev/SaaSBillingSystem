using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Domain.Entities
{
    public class Invitation
    {
        public Guid Id { get; private set; }

        public Guid OrganizationId { get; private set; }
        public Organization Organization { get; private set; } = null!;

        public string Email { get; private set; } = string.Empty;

        public string Token { get; private set; } = string.Empty;

        public OrganizationRole Role { get; private set; }

        public InvitationStatus Status { get; private set; }

        public DateTime ExpiresAtUtc { get; private set; }

        private Invitation() { }

        public Invitation(
            Guid organizationId,
            string email,
            OrganizationRole role)
        {
            Id = Guid.NewGuid();
            OrganizationId = organizationId;
            Email = email.Trim().ToLower();
            Role = role;

            Token = Guid.NewGuid().ToString();

            Status = InvitationStatus.Pending;
            ExpiresAtUtc = DateTime.UtcNow.AddDays(7);
        }

        public void Accept()
        {
            Status = InvitationStatus.Accepted;
        }

        public void Revoke()
        {
            Status = InvitationStatus.Revoked;
        }
    }
}
