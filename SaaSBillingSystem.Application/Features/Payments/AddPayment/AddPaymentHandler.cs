using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Payments.AddPayment
{
    public class AddPaymentHandler: IRequestHandler<AddPaymentCommand, Result<Guid>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        public AddPaymentHandler(IPaymentRepository paymentRepository, IInvoiceRepository invoiceRepository)
        {
            _paymentRepository = paymentRepository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Result<Guid>> Handle(AddPaymentCommand command, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(command.InvoiceId);

            if (invoice == null)
            {
                return Result<Guid>.Failure($"Invoice with id {command.InvoiceId} was not found.");
            }

            if (invoice.Status == InvoiceStatus.Cancelled)
            {
                return Result<Guid>.Failure("Cannot create payment for a cancelled invoice.");
            }

            if (invoice.Status == InvoiceStatus.Paid)
            {
                return Result<Guid>.Failure("Invoice is already paid.");
            }

            if (command.Amount != invoice.Amount)
            {
                return Result<Guid>.Failure("Payment amount does not match invoice amount.");
            }

            var paymentResult = Payment.Create(
                command.InvoiceId,
                command.Currency,
                command.Amount,
                command.Provider,
                command.ExternalPaymentId);

            if (!paymentResult.IsSuccess)
            {
                return Result<Guid>.Failure(paymentResult.Error);
            }

            var payment = paymentResult.Value;

            await _paymentRepository.AddAsync(payment!);

            return Result<Guid>.Success(payment!.Id);
        }
    }
}
