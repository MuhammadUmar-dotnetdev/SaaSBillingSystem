using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;
using System.Security.Cryptography;

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

        public DateTime CreatedAtUtc { get; private set; }

        public DateTime? AcceptedAtUtc { get; private set; }

        public DateTime? RevokedAtUtc { get; private set; }

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

        public Result Accept()
        {
            if(Status == InvitationStatus.Expired)
            {
                return Result.Failure("Expired invitation can't be accepted");
            }

            if(Status == InvitationStatus.Revoked)
            {
                return Result.Failure("Revoked invitation can't be accepted");
            }

            if (Status == InvitationStatus.Accepted)
            {
                return Result.Failure("Invitation is already accepted");
            }
            Status = InvitationStatus.Accepted;
            AcceptedAtUtc = DateTime.UtcNow;
            return Result.Success();
        }

        public Result Revoke()
        {
            if (Status == InvitationStatus.Expired)
            {
                return Result.Failure("Expired invitation can't be revoked");
            }

            if (Status == InvitationStatus.Revoked)
            {
                return Result.Failure("Invitation is already accepted");
            }
            Status = InvitationStatus.Revoked;
            RevokedAtUtc = DateTime.UtcNow;
            AcceptedAtUtc = null;
            return Result.Success();
        }

        public Result Resend()
        {
            if (Status != InvitationStatus.Pending)
            {
                return Result.Failure("Only pending invitations can be resent.");
            }

            Token = RandomNumberGenerator.GetHexString(32);

            ExpiresAtUtc = DateTime.UtcNow.AddDays(7);
            return Result.Success();
        }
    }
}
