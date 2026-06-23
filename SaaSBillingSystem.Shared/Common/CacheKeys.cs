namespace SaaSBillingSystem.Shared.Common
{
    public static class CacheKeys
    {
        public static string LoginSession(string token) => $"login-session:{token}";
    }
}
