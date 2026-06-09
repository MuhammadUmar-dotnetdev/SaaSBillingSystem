using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.RenamePlan
{
    public class RenamePlanHandler: IRequestHandler<RenamePlanCommand, Result>
    {
        private readonly IPlanRepository _planRepository;
        public RenamePlanHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }
        public async Task<Result> Handle(RenamePlanCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id);
            if(plan == null)
            {
                return Result.Failure($"Plan with id {command.Id} was not found");
            }
            plan.Rename(command.Name, command.Description);
            await _planRepository.UpdateAsync(plan);
            return Result.Success();
        }
    }
}
