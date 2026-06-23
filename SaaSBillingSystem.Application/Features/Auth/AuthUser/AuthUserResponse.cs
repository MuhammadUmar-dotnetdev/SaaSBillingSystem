namespace SaaSBillingSystem.Application.Features.Auth.AuthUser
{
    public class AuthUserResponse
    {
        public string AccessToken { get; init; } = string.Empty;

        public DateTime ExpiresAtUtc { get; init; }

        public Guid UserId { get; init; }

        public Guid OrganizationId { get; init; }

        public string Role { get; init; } = string.Empty;
    }
}
