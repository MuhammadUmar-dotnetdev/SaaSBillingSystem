using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.AddInvoice
{
    public class AddInvoiceCommand: IRequest<Result<Guid>>
    {
        public Guid OrganizationId { get; set; }
        public Guid SubscriptionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PeriodStartUtc { get; private set; }
        public DateTime PeriodEndUtc { get; private set; }
    }
}
