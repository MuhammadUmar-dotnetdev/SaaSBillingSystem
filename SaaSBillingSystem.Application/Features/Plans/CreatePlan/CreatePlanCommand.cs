using MediatR;
using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Application.Features.Plans.CreatePlan
{
    public class CreatePlanCommand: IRequest<CreatePlanResponse>
    {
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
