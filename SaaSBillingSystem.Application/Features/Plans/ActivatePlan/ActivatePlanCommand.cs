using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.ActivatePlan
{
    public class ActivatePlanCommand: IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
