using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.EnablePlanFeature
{
    public class EnablePlanFeatureCommand: IRequest<Result>
    {
        public Guid PlanId { get; set; }
        public Guid FeatureId { get; set; } 
    }
}
