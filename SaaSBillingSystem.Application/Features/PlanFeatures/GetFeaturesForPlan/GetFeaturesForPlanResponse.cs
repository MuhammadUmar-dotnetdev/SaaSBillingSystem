namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetFeaturesForPlan
{
    public class GetFeaturesForPlanResponse
    {
        public Guid FeatureId { get; init; }

        public string Key { get; init; } = string.Empty;

        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public bool IsEnabled { get; init; }

        public int? Limit { get; init; }

        public string? Unit { get; init; }
    }
}
