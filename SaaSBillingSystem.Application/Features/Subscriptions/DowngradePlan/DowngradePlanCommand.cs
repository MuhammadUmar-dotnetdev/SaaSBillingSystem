using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Subscriptions.DowngradePlan
{
    public class DowngradePlanCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public Guid NewPlanId { get; private set; }
        public DowngradePlanCommand(Guid id, Guid newPlanId)
        {
            Id = id;
            NewPlanId = newPlanId;
        }
    }
}
