using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetPlanFeatureById
{
    public class GetPlanFeatureByIdCommand: IRequest<Result<GetPlanFeatureByIdResponse>>
    {
        public Guid Id { get; private set; }
        public GetPlanFeatureByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
