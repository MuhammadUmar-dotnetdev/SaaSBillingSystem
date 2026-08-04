namespace SaaSBillingSystem.Application.DTOs
{
    public class InvitationDTO
    {
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime ExpiresAtUtc { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? AcceptedAtUtc { get; set; }

        public DateTime? RevokedAtUtc { get; set; }
    }
}
