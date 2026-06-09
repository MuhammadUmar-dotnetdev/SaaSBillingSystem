using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.DisablePlanFeature
{
    public class DisablePlanFeatureHandler: IRequestHandler<DisablePlanFeatureCommand, Result>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        public DisablePlanFeatureHandler(IPlanFeatureRepository planFeatureRepository)
        {
            _planFeatureRepository = planFeatureRepository;
        }

        public async Task<Result> Handle(DisablePlanFeatureCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdAsync(command.Id);
            if (planFeature == null)
            {
                return Result.Failure($"Planfeature with id {command.Id} was not found");
            }
            planFeature.Disable();
            await _planFeatureRepository.UpdateAsync(planFeature);
            return Result.Success();
        }
    }
}
