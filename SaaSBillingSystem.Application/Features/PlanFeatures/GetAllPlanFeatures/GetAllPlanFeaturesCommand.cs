using MediatR;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetAllPlanFeatures
{
    public class GetAllPlanFeaturesCommand: IRequest<Result<List<GetAllPlanFeaturesResponse>>>
    {
    }
}
