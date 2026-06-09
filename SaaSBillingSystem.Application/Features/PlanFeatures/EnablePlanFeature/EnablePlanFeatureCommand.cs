using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.EnablePlanFeature
{
    public class EnablePlanFeatureCommand: IRequest<Result>
    {
        public Guid Id { get; private set; }
        public EnablePlanFeatureCommand(Guid id)
        {
            Id = id;
        }
    }
}
