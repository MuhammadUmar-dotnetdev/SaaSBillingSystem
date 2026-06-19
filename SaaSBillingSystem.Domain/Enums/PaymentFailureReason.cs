namespace SaaSBillingSystem.Domain.Enums
{
    public enum PaymentFailureReason
    {
        CardDeclined = 0,
        InsufficientFunds = 1,
        ExpiredCard = 2,
        NetworkError = 3,
    }
}
