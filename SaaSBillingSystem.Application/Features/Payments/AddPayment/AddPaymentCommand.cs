using MediatR;
using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.AddPayment
{
    public class AddPaymentCommand: IRequest<Result<Guid>>
    {
        public Guid InvoiceId { get; set; }
        public PaymentCurrency Currency { get; set; }
        public decimal Amount { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string ExternalPaymentId { get; set; } = string.Empty;
    }
}
