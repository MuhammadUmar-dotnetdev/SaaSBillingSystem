using MediatR;
using SaaSBillingSystem.Application.Features.Payments.PaymentDTOs;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.GetPaymentById
{
    public class GetPaymentByIdCommand: IRequest<Result<PaymentResponse>>
    {
        public Guid Id { get; private set; }
        public GetPaymentByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
