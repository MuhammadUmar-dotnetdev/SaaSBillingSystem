using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.GetPlanById
{
    public class GetPlanByIdCommand: IRequest<Result<GetPlanByIdResponse>>
    {
        public Guid Id { get; set; }
        public GetPlanByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
