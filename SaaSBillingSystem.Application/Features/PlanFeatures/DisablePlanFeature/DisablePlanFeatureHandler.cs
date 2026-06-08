using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.DisablePlanFeature
{
    public class DisablePlanFeatureHandler: IRequestHandler<DisablePlanFeatureCommand, Result<bool>>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        public DisablePlanFeatureHandler(IPlanFeatureRepository planFeatureRepository)
        {
            _planFeatureRepository = planFeatureRepository;
        }

        public async Task<Result<bool>> Handle(DisablePlanFeatureCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdAsync(command.Id);
            if (planFeature == null)
            {
                return Result<bool>.Failure($"Planfeature with id {command.Id} was not found");
            }
            planFeature.Disable();
            await _planFeatureRepository.UpdateAsync(planFeature);
            return Result<bool>.Success(true);
        }
    }
}
