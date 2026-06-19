using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.MarkPaymentAsPaid
{
    public class MarkPaymentAsPaidHandler: IRequestHandler<MarkPaymentAsPaidCommand, Result>
    {
        private readonly IPaymentRepository _paymentRepository;
        public MarkPaymentAsPaidHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result> Handle(MarkPaymentAsPaidCommand command, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(command.Id);
            if (payment == null)
            {
                return Result.Failure($"Payment with {command.Id} not found");
            }

            var result = payment.MarkAsPaid();
            if (!result.IsSuccess)
            {
                return result;
            }
            await _paymentRepository.UpdateAsync(payment);
            return Result.Success();
        }
    }
}
