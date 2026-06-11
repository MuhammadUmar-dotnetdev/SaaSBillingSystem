using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.UpgradePlan
{
    public class UpgradePlanCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public Guid NewPlanId { get; private set; }
        public UpgradePlanCommand(Guid id, Guid newPlanId)
        {
            Id = id;
            NewPlanId = newPlanId;
        }
    }
}
