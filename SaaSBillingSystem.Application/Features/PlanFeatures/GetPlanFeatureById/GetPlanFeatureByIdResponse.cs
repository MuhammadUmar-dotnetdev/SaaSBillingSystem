namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetPlanFeatureById
{
    public class GetPlanFeatureByIdResponse
    {
        public Guid PlanId { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsEnabled { get; set; } = true;
        public int? Limit { get; set; } = null;
        public string? Unit { get; set; } = null;
    }
}
