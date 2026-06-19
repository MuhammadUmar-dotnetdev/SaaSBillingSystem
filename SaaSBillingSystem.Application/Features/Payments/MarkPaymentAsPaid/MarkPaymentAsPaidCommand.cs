using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.MarkPaymentAsPaid
{
    public class MarkPaymentAsPaidCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public MarkPaymentAsPaidCommand(Guid id)
        {
            Id = id;
        }
    }
}
