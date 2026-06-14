using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.AddInvoice
{
    public class AddInvoiceHandler: IRequestHandler<AddInvoiceCommand, Result<Guid>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public AddInvoiceHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Result<Guid>> Handle(AddInvoiceCommand command, CancellationToken cancellationToken)
        {
            if(await _invoiceRepository.ExistsForPeriodAsync(command.SubscriptionId, command.PeriodStartUtc, command.PeriodEndUtc))
            {
                return Result<Guid>.Failure("An invoice for this subscription already with given period");
            }

            var invoiceResult = Invoice.Create(
                    command.OrganizationId,
                    command.SubscriptionId,
                    command.Amount,
                    command.DueDate,
                    command.PeriodStartUtc,
                    command.PeriodEndUtc
                );

            if (!invoiceResult.IsSuccess)
            {
                return Result<Guid>.Failure(invoiceResult.Error);
            }

            await _invoiceRepository.AddAsync(invoiceResult.Value!);
            return Result<Guid>.Success(invoiceResult.Value!.Id);
        }
    }
}
