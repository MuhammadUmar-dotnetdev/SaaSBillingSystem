using MediatR;
using SaaSBillingSystem.Application.Features.Invoices.InvoicesDTOs;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.GetInvoicesBySubscription
{
    public class GetInvoicesBySubscriptionCommand: IRequest<Result<List<InvoiceResponse>>>
    {
        public Guid SubscriptionId { get; private set; }

        public GetInvoicesBySubscriptionCommand(Guid subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }
    }
}
