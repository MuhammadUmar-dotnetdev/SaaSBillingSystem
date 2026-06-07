using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.ActivatePlan
{
    public class ActivatePlanCommand: IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
