using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.MarkPaymentAsFailed
{
    public class MarkPaymentAsFailedHandler: IRequestHandler<MarkPaymentAsFailedCommand, Result>
    {
        private readonly IPaymentRepository _paymentRepository;
        public MarkPaymentAsFailedHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result> Handle(MarkPaymentAsFailedCommand command, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(command.Id);
            if (payment == null)
            {
                return Result.Failure($"Payment with {command.Id} not found");
            }

            var result = payment.MarkAsFailed(command.FailureReason);
            if (!result.IsSuccess)
            {
                return result;
            }
            await _paymentRepository.UpdateAsync(payment);
            return Result.Success();
        }
    }
}
