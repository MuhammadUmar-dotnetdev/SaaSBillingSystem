using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.InvoiceExistsForPeriod
{
    public class InvoiceExistsForPeriodCommand: IRequest<Result>
    {
        public Guid SubscriptionId { get; set; }
        public DateTime PeriodStartUtc { get; set; }
        public DateTime PeriodEndUtc { get; set; }
    }
}
