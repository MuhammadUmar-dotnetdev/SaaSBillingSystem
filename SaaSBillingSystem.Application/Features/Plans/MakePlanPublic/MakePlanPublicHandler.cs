using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.MakePlanPublic
{
    public class MakePlanPublicHandler: IRequestHandler<MakePlanPublicCommand, Result>
    {
        private readonly IPlanRepository _planRepository;
        public MakePlanPublicHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Result> Handle(MakePlanPublicCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id);
            if (plan == null)
            {
                return Result.Failure($"Plan with id {command.Id} was not found");
            }
            plan.MakePublic();
            await _planRepository.UpdateAsync(plan);
            return Result.Success();
        }
    }
}
