using MediatR;
using SaaSBillingSystem.Application.Features.Payments.PaymentDTOs;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.GetAllPayments
{
    public class GetAllPaymentsHandler: IRequestHandler<GetAllPaymentsCommand, Result<List<PaymentResponse>>>
    {
        private readonly IPaymentRepository _paymentRepository;
        public GetAllPaymentsHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result<List<PaymentResponse>>> Handle(GetAllPaymentsCommand command, CancellationToken cancellationToken)
        {
            var paymentsList = await _paymentRepository.GetAllAsync();
            if (paymentsList.Count == 0)
            {
                return Result<List<PaymentResponse>>.Failure("No payments exists in system");
            }

            var response = paymentsList.Select(p => new PaymentResponse
            {
                Id = p.Id,
                InvoiceId = p.InvoiceId,
                Amount = p.Amount,
                Provider = p.Provider,
                PaidAt = p.PaidAt,
                RefundedAt = p.RefundedAt,
                Status = p.Status,
                ExternalPaymentId = p.ExternalPaymentId,
                Currency = p.Currency,
                FailureReason = p.FailureReason,
            }).ToList();

            return Result<List<PaymentResponse>>.Success(response);
        }
    }
}
