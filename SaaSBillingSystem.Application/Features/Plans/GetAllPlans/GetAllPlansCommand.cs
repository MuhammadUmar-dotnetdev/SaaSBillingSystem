using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.GetAllPlans
{
    public class GetAllPlansCommand: IRequest<Result<List<GetAllPlansResponse>>>
    {

    }
}
