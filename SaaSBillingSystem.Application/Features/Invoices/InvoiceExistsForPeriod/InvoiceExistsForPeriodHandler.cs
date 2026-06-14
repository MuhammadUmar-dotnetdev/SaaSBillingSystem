using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.InvoiceExistsForPeriod
{
    public class InvoiceExistsForPeriodHandler: IRequestHandler<InvoiceExistsForPeriodCommand, Result>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceExistsForPeriodHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Result> Handle(InvoiceExistsForPeriodCommand command, CancellationToken cancellationToken)
        {
            var result = await _invoiceRepository.ExistsForPeriodAsync(command.SubscriptionId, command.PeriodStartUtc, command.PeriodEndUtc);

            if(!result)
            {
                return Result.Failure($"Invoice having subscription id {command.SubscriptionId} with period between {command.PeriodStartUtc} and {command.PeriodEndUtc} don't exists");
            }
            return Result.Success();
        }
    }
}
