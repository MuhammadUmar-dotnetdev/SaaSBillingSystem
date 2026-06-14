using MediatR;
using SaaSBillingSystem.Application.Features.Invoices.InvoicesDTOs;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.GetAllInvoices
{
    public class GetAllInvoicesHandler: IRequestHandler<GetAllInvoicesCommand, Result<List<InvoiceResponse>>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IPlanRepository _planRepository;
        public GetAllInvoicesHandler(IInvoiceRepository invoiceRepository, ISubscriptionRepository subscriptionRepository, IOrganizationRepository organizationRepository, IPlanRepository planRepository)
        {
            _invoiceRepository = invoiceRepository;
            _subscriptionRepository = subscriptionRepository;
            _organizationRepository = organizationRepository;
            _planRepository = planRepository;
        }

        public async Task<Result<List<InvoiceResponse>>> Handle(GetAllInvoicesCommand command, CancellationToken cancellationToken)
        {
            var invoicesList = await _invoiceRepository.GetAllAsync();
            if(invoicesList.Count == 0)
            {
                return Result<List<InvoiceResponse>>.Failure("Invoice list is empty");
            }

            var subscriptionIds = invoicesList.Select(i => i.SubscriptionId).Distinct().ToList();
            var subscriptions = await _subscriptionRepository.GetByIdsAsync(subscriptionIds);

            var organizationIds = invoicesList.Select(i => i.OrganizationId).Distinct().ToList();
            var organizations = await _organizationRepository.GetByIdsAsync(organizationIds);
            var orgDict = organizations.ToDictionary(o => o.Id, o => o.Name);

            var planIds = subscriptions.Select(s => s.PlanId).Distinct().ToList();
            var plans = await _planRepository.GetByIdsAsync(planIds);
            var planDict = plans.ToDictionary(p => p.Id, p => p.Name);

            var subcriptionPlanDict = subscriptions.ToDictionary(s => s.Id, s => s.PlanId);

            var response = invoicesList.Select(i =>
            {
                var planId = subcriptionPlanDict[i.SubscriptionId];

                return new InvoiceResponse
                {

                    Id = i.Id,
                    OrganizationId = i.OrganizationId,
                    SubscriptionId = i.SubscriptionId,
                    PlanId = planId,
                    OrganizationName = orgDict[i.OrganizationId],
                    PlanName = planDict[planId],
                    Amount = i.Amount,
                    IssuedAt = i.IssuedAt,
                    DueDate = i.DueDate,
                    Status = i.Status,
                    PaidAtUtc = i.PaidAtUtc,
                    CancelledAtUtc = i.CancelledAtUtc,
                    OverdueAtUtc = i.OverdueAtUtc,
                    PeriodStartUtc = i.PeriodStartUtc,
                    PeriodEndUtc = i.PeriodEndUtc,
                    InvoiceNumber = i.InvoiceNumber
                };
            }).ToList();

            return Result<List<InvoiceResponse>>.Success(response);
        }
    }
}
