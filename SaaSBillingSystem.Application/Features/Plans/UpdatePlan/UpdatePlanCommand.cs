using MediatR;
using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.UpdatePlan
{
    public class UpdatePlanCommand: IRequest<Result<UpdatePlanResponse>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public BillingCycle BillingCycle { get; set; }
        public int MaxUsers { get; set; }
        public int MaxProjects { get; set; }
        public long MaxStorageInMb { get; set; }
        public bool IsPublic { get; set; }
    }
}
