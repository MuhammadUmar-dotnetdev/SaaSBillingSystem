using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetPlansForFeature
{
    public class GetPlansForFeatureCommand: IRequest<Result<List<GetPlansForFeatureResponse>>>
    {
        public Guid FeatureId { get; private set; }
        public GetPlansForFeatureCommand(Guid featureId)
        {
            FeatureId = featureId;
        }
    }
}
