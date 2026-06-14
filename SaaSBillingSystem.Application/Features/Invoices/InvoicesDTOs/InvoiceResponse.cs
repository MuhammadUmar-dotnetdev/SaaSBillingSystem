using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Application.Features.Invoices.InvoicesDTOs
{
    public class InvoiceResponse
    {
        public Guid Id { get; set; }

        public Guid OrganizationId { get; set; }
        public Guid SubscriptionId { get; set; }
        public Guid PlanId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;

        public string PlanName { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime IssuedAt { get; set; }

        public DateTime DueDate { get; set; }

        public InvoiceStatus Status { get; set; }
        public DateTime? PaidAtUtc { get; set; }
        public DateTime? CancelledAtUtc { get; set; }
        public DateTime? OverdueAtUtc { get; set; }
        public DateTime PeriodStartUtc { get; set; }
        public DateTime PeriodEndUtc { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
    }
}
