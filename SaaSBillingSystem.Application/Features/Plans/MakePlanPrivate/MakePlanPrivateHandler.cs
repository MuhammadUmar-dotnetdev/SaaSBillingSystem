using MediatR;
using SaaSBillingSystem.Application.Features.Plans.UpdatePlan;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.MakePlanPrivate
{
    public class MakePlanPrivateHandler: IRequestHandler<MakePlanPrivateCommand, Result<bool>>
    {
        private readonly IPlanRepository _planRepository;
        public MakePlanPrivateHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Result<bool>> Handle(MakePlanPrivateCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id);
            if (plan == null)
            {
                return Result<bool>.Failure($"Plan with id {command.Id} was not found");
            }
            plan.MakePrivate();
            await _planRepository.UpdateAsync(plan);
            return Result<bool>.Success(true);
        }
    }
}
