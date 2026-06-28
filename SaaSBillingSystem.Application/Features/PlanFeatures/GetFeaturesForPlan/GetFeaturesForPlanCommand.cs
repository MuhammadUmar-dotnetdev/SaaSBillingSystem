using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetFeaturesForPlan
{
    public class GetFeaturesForPlanCommand: IRequest<Result<List<GetFeaturesForPlanResponse>>>
    {
        public Guid PlanId { get; private set; }
        public GetFeaturesForPlanCommand(Guid planId)
        {
            PlanId = planId;
        }
    }
}
