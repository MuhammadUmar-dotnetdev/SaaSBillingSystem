using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Domain.Entities
{
    public class PlanFeature
    {
        public Guid PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
        public Guid FeatureId { get; set; }
        public Feature Feature { get; set; } = null!;
        public bool IsEnabled { get; private set; }
        public int? Limit { get; private set; }
        public string? Unit { get; private set; }
        private PlanFeature() { }

        public PlanFeature(Guid planId, Guid featureId, bool isEnabled, int? limit, string? unit)
        {
            PlanId = planId;
            FeatureId = featureId;
            IsEnabled = isEnabled;
            Limit = limit;
            Unit = unit;
        }

        public Result Enable()
        {
            if (IsEnabled == true)
            {
                return Result.Failure("This PlanFeature is already enabled");
            }
            return Result.Success();
        }
        public Result Disable()
        {
            if (IsEnabled == false)
            {
                return Result.Failure("This PlanFeature is already disabled");
            }
            return Result.Success();
        }

        public Result ChangeLimits(int limit)
        {
            if (Limit == limit)
            {
                return Result.Failure("Limit is already set to given value");
            }
            Limit = limit;
            return Result.Success();
        }
    }
}
