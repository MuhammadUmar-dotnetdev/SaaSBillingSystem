using MediatR;
using SaaSBillingSystem.Application.Features.Payments.PaymentDTOs;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.GetAllPayments
{
    public class GetAllPaymentsCommand: IRequest<Result<List<PaymentResponse>>>
    {

    }
}
