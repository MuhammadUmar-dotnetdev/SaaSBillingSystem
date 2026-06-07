using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.MakePlanPublic
{
    public class MakePlanPublicCommand: IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }
}
