namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetPlansForFeature
{
    public class GetPlansForFeatureResponse
    {
        public Guid PlanId { get; init; }

        public string PlanName { get; init; } = string.Empty;

        public string PlanDescription { get; init; } = string.Empty;

        public bool IsEnabled { get; init; }

        public int? Limit { get; init; }

        public string? Unit { get; init; }
    }
}
