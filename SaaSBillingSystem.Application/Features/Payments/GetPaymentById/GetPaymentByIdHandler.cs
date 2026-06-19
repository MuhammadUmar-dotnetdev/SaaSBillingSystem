using MediatR;
using SaaSBillingSystem.Application.Features.Payments.GetPaymentById;
using SaaSBillingSystem.Application.Features.Payments.PaymentDTOs;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.GetPaymentsById
{
    public class GetPaymentsByIdHandler: IRequestHandler<GetPaymentByIdCommand, Result<PaymentResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;
        public GetPaymentsByIdHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result<PaymentResponse>> Handle(GetPaymentByIdCommand command, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(command.Id);
            
            if (payment == null)
            {
                return Result<PaymentResponse>.Failure($"Payment with {command.Id} not found");
            }

            var response = new PaymentResponse
            {
                Id = payment.Id,
                InvoiceId = payment.InvoiceId,
                Amount = payment.Amount,
                Provider = payment.Provider,
                PaidAt = payment.PaidAt,
                RefundedAt = payment.RefundedAt,
                Status = payment.Status,
                ExternalPaymentId = payment.ExternalPaymentId,
                Currency = payment.Currency,
                FailureReason = payment.FailureReason,
            };

            return Result<PaymentResponse>.Success(response);
        }
    }
}
