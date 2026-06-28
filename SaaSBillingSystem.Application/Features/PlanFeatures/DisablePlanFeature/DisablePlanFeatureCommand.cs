using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.DisablePlanFeature
{
    public class DisablePlanFeatureCommand: IRequest<Result>
    {
        public Guid PlanId { get; set; }
        public Guid FeatureId { get; set; }
    }
}
