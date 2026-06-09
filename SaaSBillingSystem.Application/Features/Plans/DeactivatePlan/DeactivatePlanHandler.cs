using MediatR;
using SaaSBillingSystem.Application.Features.Plans.UpdatePlan;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.DeactivatePlan
{
    public class DeactivatePlanHandler: IRequestHandler<DeactivatePlanCommand, Result>
    {
        private readonly IPlanRepository _planRepository;
        public DeactivatePlanHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }
        public async Task<Result> Handle(DeactivatePlanCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id);
            if (plan == null)
            {
                return Result.Failure($"Plan with id {command.Id} was not found");
            }
            plan.Deactivate();
            await _planRepository.UpdateAsync(plan);
            return Result.Success();
        }
    }
}
