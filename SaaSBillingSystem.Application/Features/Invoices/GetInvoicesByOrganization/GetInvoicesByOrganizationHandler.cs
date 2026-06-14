using MediatR;
using SaaSBillingSystem.Application.Features.Invoices.InvoicesDTOs;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.GetInvoicesByOrganization
{
    public class GetInvoicesByOrganizationHandler: IRequestHandler<GetInvoicesByOrganizationCommand, Result<List<InvoiceResponse>>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        public readonly IPlanRepository _planRepository;
        public GetInvoicesByOrganizationHandler(IInvoiceRepository invoiceRepository, IOrganizationRepository organizationRepository, ISubscriptionRepository subscriptionRepository, IPlanRepository planRepository)
        {
            _invoiceRepository = invoiceRepository;
            _organizationRepository = organizationRepository;
            _subscriptionRepository = subscriptionRepository;
            _planRepository = planRepository;
        }

        public async Task<Result<List<InvoiceResponse>>> Handle(GetInvoicesByOrganizationCommand command, CancellationToken cancellationToken)
        {
            var invoicesList = await _invoiceRepository.GetByOrganizationAsync(command.OrganizationId);
            if (invoicesList.Count == 0)
            {
                return Result<List<InvoiceResponse>>.Failure($"Invoice list is empty for id {command.OrganizationId}");
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
