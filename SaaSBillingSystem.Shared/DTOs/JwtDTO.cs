namespace SaaSBillingSystem.Shared.DTOs
{
    public class JwtDTO
    {
        public string AccessToken { get; init; } = string.Empty;

        public DateTime ExpiresAtUtc { get; init; }
    }
}
