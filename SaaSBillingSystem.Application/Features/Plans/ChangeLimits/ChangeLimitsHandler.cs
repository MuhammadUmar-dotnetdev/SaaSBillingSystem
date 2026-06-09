using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.ChangeLimits
{
    public class ChangeLimitsHandler: IRequestHandler<ChangeLimitsCommand, Result>
    {
        private readonly IPlanRepository _planRepository;
        public ChangeLimitsHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Result> Handle(ChangeLimitsCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id);
            if(plan == null)
            {
                return Result.Failure($"Plan with id {command.Id} was not found");
            }
            plan.ChangeLimits(command.MaxUsers, command.MaxProjects, command.MaxStorageInMb);
            await _planRepository.UpdateAsync(plan);
            return Result.Success();
        }
    }
}
