using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.DisablePlanFeature
{
    public class DisablePlanFeatureHandler: IRequestHandler<DisablePlanFeatureCommand, Result>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        private readonly ILogger<DisablePlanFeatureHandler> _logger;

        public DisablePlanFeatureHandler(IPlanFeatureRepository planFeatureRepository, ILogger<DisablePlanFeatureHandler> logger)
        {
            _planFeatureRepository = planFeatureRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(DisablePlanFeatureCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdsAsync(command.PlanId, command.FeatureId, cancellationToken);
            if (planFeature == null)
            {
                _logger.LogWarning("PlanFeature with PlanId {PlanId} and FeatureId {FeatureId} is not found", command.PlanId, command.FeatureId);
                return Result.Failure("PlanFeature with given PlanId and FeatureId Is Not Found");
            }
            planFeature.Disable();
            await _planFeatureRepository.UpdateAsync(planFeature, cancellationToken);
            _logger.LogInformation("PlanFeature with PlanId {PlanId} and FeatureId {FeatureId} is successfully disabled", command.PlanId, command.FeatureId);

            return Result.Success();
        }
    }
}
