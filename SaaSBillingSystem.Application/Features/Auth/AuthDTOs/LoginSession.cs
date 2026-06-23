namespace SaaSBillingSystem.Application.Features.Auth.AuthDTOs
{
    public sealed class LoginSession
    {
        public Guid UserId { get; init; }

        public string Email { get; init; } = string.Empty;

        public DateTime CreatedAtUtc { get; init; }
    }
}
