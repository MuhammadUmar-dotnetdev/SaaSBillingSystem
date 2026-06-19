using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.HasSuccessfulPayment
{
    public class HasSuccessfulPaymentHandler: IRequestHandler<HasSuccessfulPaymentCommand, Result>
    {
        private readonly IPaymentRepository _paymentRepository;
        public HasSuccessfulPaymentHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result> Handle(HasSuccessfulPaymentCommand command, CancellationToken cancellationToken)
        {
            var result = await _paymentRepository.HasSuccessfulPaymentAsync(command.InvoiceId);
            if (!result)
            {
                return Result.Failure($"No successful payment with {command.InvoiceId} exists");
            }

            return Result.Success();
        }
    }
}
