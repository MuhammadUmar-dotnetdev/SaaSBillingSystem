using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.AddPlanFeature
{
    public class AddPlanFeatureCommand: IRequest<Result>
    {
        public Guid PlanId { get; set; }
        public Guid FeatureId { get; set; }
        public bool IsEnabled { get; set; }
        public int? Limit { get; set; }
        public string? Unit { get; set; }
    }
}
