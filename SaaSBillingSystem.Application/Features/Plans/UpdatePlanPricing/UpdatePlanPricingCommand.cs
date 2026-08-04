using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.UpdatePlanPricing
{
    public class UpdatePlanPricingCommand: IRequest<Result>
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
    }
}
