using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.DeactivatePlan
{
    public class DeactivatePlanCommand: IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
