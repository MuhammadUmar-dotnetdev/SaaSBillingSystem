using SaaSBillingSystem.Application.Features.PlanFeatures.GetFeaturesForPlan;
using SaaSBillingSystem.Application.Features.PlanFeatures.GetPlansForFeature;

namespace SaaSBillingSystem.Application.Queries
{
    public interface IPlanFeatureQueries
    {
        Task<List<GetPlansForFeatureResponse>> GetPlansForFeatureAsync(Guid featureId, CancellationToken cancellationToken);
        Task<List<GetFeaturesForPlanResponse>> GetFeaturesForPlanAsync(Guid planId, CancellationToken cancellationToken);
    }
}
