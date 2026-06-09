using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.ChangeLimits
{
    public class ChangeLimitsCommand: IRequest<Result>
    {
        public Guid Id { get; set; }
        public int MaxUsers { get; set; }
        public int MaxProjects { get; set; }
        public long MaxStorageInMb { get; set; }
    }
}
