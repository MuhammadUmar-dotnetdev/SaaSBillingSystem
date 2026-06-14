using MediatR;
using SaaSBillingSystem.Application.Features.Invoices.InvoicesDTOs;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.GetInvoiceById
{
    public class GetInvoiceByIdHandler: IRequestHandler<GetInvoiceByIdCommand, Result<InvoiceResponse>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        public readonly IPlanRepository _planRepository;
        public GetInvoiceByIdHandler(IInvoiceRepository invoiceRepository, IOrganizationRepository organizationRepository, ISubscriptionRepository subscriptionRepository, IPlanRepository planRepository)
        {
            _invoiceRepository = invoiceRepository;
            _organizationRepository = organizationRepository;
            _subscriptionRepository = subscriptionRepository;
            _planRepository = planRepository;
        }

        public async Task<Result<InvoiceResponse>> Handle(GetInvoiceByIdCommand command, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(command.Id);
            if(invoice == null)
            {
                return Result<InvoiceResponse>.Failure($"Invoice with id {command.Id} was not found");
            }
            var organization = await _organizationRepository.GetByIdAsync(invoice.OrganizationId);

            if (organization == null)
            {
                return Result<InvoiceResponse>.Failure($"Organization with id {invoice.OrganizationId} was not found");
            }

            var subscription = await _subscriptionRepository.GetByIdAsync(invoice.SubscriptionId);

            if (subscription == null)
            {
                return Result<InvoiceResponse>.Failure($"Subscription with id {invoice.SubscriptionId} was not found");
            }

            var plan = await _planRepository.GetPlanByIdAsync(subscription.PlanId);

            if (plan == null)
            {
                return Result<InvoiceResponse>.Failure($"Plan with id {subscription.PlanId} was not found");
            }

            var response = new InvoiceResponse
            {
                Id = invoice.Id,
                OrganizationId = invoice.OrganizationId,
                SubscriptionId = invoice.SubscriptionId,
                PlanId = plan.Id,
                OrganizationName = organization.Name,
                PlanName = plan.Name,
                Amount = invoice.Amount,
                IssuedAt = invoice.IssuedAt,
                DueDate = invoice.DueDate,
                Status = invoice.Status,
                PaidAtUtc = invoice.PaidAtUtc,
                CancelledAtUtc = invoice.CancelledAtUtc,
                OverdueAtUtc = invoice.OverdueAtUtc,
                PeriodStartUtc = invoice.PeriodStartUtc,
                PeriodEndUtc = invoice.PeriodEndUtc,
                InvoiceNumber = invoice.InvoiceNumber
            };

            return Result<InvoiceResponse>.Success(response);
        }
    }
}
