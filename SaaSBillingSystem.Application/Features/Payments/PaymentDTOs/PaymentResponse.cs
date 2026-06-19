using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Application.Features.Payments.PaymentDTOs
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }

        public Guid InvoiceId { get; set; }

        public decimal Amount { get; set; }

        public string Provider { get; set; } = string.Empty;

        public DateTime? PaidAt { get; set; }
        public DateTime? RefundedAt { get; set; }
        public PaymentStatus Status { get; set; }

        public string? ExternalPaymentId { get; set; }

        public PaymentCurrency Currency { get; set; } = PaymentCurrency.USD;
        public string? FailureReason { get; set; }
    }
}
