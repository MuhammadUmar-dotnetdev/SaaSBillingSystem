using MediatR;
using SaaSBillingSystem.Application.Features.Payments.PaymentDTOs;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.GetPaymentsByInvoiceId
{
    public class GetPaymentsByInvoiceIdHandler: IRequestHandler<GetPaymentsByInvoiceIdCommand, Result<List<PaymentResponse>>>
    {
        private readonly IPaymentRepository _paymentRepository;
        public GetPaymentsByInvoiceIdHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result<List<PaymentResponse>>> Handle(GetPaymentsByInvoiceIdCommand command, CancellationToken cancellationToken)
        {
            var paymentsList = await _paymentRepository.GetByInvoiceIdAsync(command.InvoiceId);
            if (paymentsList.Count == 0)
            {
                return Result<List<PaymentResponse>>.Failure("No payments for this invoice");
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
