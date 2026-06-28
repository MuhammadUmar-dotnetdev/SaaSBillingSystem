using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.FeaturesForPlan.AddFeature
{
    public class AddFeatureCommand: IRequest<Result<Guid>>
    {
        public string Key { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
