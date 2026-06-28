using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.RemovePlanFeature
{
    public class RemovePlanFeatureCommand: IRequest<Result>
    {
        public Guid PlanId { get; set; }
        public Guid FeatureId { get; set; }
    }
}
