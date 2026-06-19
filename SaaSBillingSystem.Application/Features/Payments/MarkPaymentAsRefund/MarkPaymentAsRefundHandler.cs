using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.MarkPaymentAsRefund
{
    public class MarkPaymentAsRefundHandler: IRequestHandler<MarkPaymentAsRefundCommand, Result>
    {
        private readonly IPaymentRepository _paymentRepository;
        public MarkPaymentAsRefundHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result> Handle(MarkPaymentAsRefundCommand command, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(command.Id);
            if (payment == null)
            {
                return Result.Failure($"Payment with {command.Id} not found");
            }

            var result = payment.MarkAsRefund();
            if (!result.IsSuccess)
            {
                return result;
            }
            await _paymentRepository.UpdateAsync(payment);
            return Result.Success();
        }
    }
}
