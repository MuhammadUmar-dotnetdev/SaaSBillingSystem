using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.HasSuccessfulPayment
{
    public class HasSuccessfulPaymentCommand: IRequest<Result>
    {
        public Guid InvoiceId { get; private set; }
        public HasSuccessfulPaymentCommand(Guid invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}
