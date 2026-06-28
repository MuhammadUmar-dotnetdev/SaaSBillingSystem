using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.ChangePlanFeatureLimits
{
    public class ChangePlanFeatureLimitsCommand: IRequest<Result>
    {
        public Guid PlanId { get; set; }
        public Guid FeatureId { get; set; }
        public int Limit { get; set; }
    }
}
