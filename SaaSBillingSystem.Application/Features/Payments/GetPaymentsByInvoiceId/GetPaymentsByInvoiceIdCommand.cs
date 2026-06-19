using MediatR;
using SaaSBillingSystem.Application.Features.Payments.PaymentDTOs;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.GetPaymentsByInvoiceId
{
    public class GetPaymentsByInvoiceIdCommand: IRequest<Result<List<PaymentResponse>>>
    {
        public Guid InvoiceId { get; private set; }

        public GetPaymentsByInvoiceIdCommand(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}
