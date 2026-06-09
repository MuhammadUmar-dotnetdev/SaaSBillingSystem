using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.MakePlanPrivate
{
    public class MakePlanPrivateCommand: IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
