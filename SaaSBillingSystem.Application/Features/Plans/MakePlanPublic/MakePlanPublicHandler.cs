using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Plans.MakePlanPublic
{
    public class MakePlanPublicHandler: IRequestHandler<MakePlanPublicCommand, Result<bool>>
    {
        private readonly IPlanRepository _planRepository;
        public MakePlanPublicHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Result<bool>> Handle(MakePlanPublicCommand command, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetPlanByIdAsync(command.Id);
            if (plan == null)
            {
                return Result<bool>.Failure($"Plan with id {command.Id} was not found");
            }
            plan.MakePublic();
            await _planRepository.UpdateAsync(plan);
            return Result<bool>.Success(true);
        }
    }
}
