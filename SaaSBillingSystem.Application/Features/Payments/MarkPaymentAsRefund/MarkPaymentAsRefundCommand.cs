using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.MarkPaymentAsRefund
{
    public class MarkPaymentAsRefundCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public MarkPaymentAsRefundCommand(Guid id)
        {
            Id = id;
        }
    }
}
