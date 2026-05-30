namespace SaaSBillingSystem.Domain.Enums;

public enum SubscriptionStatus
{
    Pending = 1,
    Active = 2,
    Trialing = 3,
    PastDue = 4,
    Cancelled = 5,
    Expired = 6,
    Suspended = 7
}