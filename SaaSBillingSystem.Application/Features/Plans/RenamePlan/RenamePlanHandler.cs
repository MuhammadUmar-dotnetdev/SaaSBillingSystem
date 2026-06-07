using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.RenamePlan
{
    public class RenamePlanHandler: IRequestHandler<RenamePlanCommand, Result<bool>>
    {
        private readonly IPlanRepository _planRepository;
        public RenamePlanHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }
        public async Task<Result<bool>> Handle(RenamePlanCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id);
            if(plan == null)
            {
                return Result<bool>.Failure($"Plan with id {command.Id} was not found");
            }
            plan.Rename(command.Name, command.Description);
            await _planRepository.UpdateAsync(plan);
            return Result<bool>.Success(true);
        }
    }
}
