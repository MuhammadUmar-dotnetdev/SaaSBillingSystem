using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.DisablePlanFeature
{
    public class DisablePlanFeatureCommand: IRequest<Result<bool>>
    {
        public Guid Id { get; private set; }
        public DisablePlanFeatureCommand(Guid id)
        {
            Id = id;
        }
    }
}
