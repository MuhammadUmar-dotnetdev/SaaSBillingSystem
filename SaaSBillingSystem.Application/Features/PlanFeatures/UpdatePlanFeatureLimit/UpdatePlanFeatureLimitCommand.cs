using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.UpdatePlanFeatureLimit
{
    public class UpdatePlanFeatureLimitCommand: IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public int Limit { get; set; }
        public string Unit {  get; set; } = string.Empty;
    }
}
