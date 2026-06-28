using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.ChangePlanFeatureLimits
{
    public class ChangePlanFeatureLimitsHandler: IRequestHandler<ChangePlanFeatureLimitsCommand, Result>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        private readonly ILogger<ChangePlanFeatureLimitsHandler> _logger;
        public ChangePlanFeatureLimitsHandler(IPlanFeatureRepository planFeatureRepository, ILogger<ChangePlanFeatureLimitsHandler> logger)
        {
            _planFeatureRepository = planFeatureRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(ChangePlanFeatureLimitsCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdsAsync(command.PlanId, command.FeatureId, cancellationToken);
            if (planFeature == null)
            {
                _logger.LogWarning("PlanFeature with PlanId {PlanId} and FeatureId {FeatureId} is not found", command.PlanId, command.FeatureId);
                return Result.Failure("PlanFeature with given PlanId and FeatureId Is Not Found");
            }
            planFeature.ChangeLimits(command.Limit);
            await _planFeatureRepository.UpdateAsync(planFeature, cancellationToken);
            _logger.LogInformation("PlanFeature with PlanId {PlanId} and FeatureId {FeatureId} successfully updated with new limit of {Limit}", command.PlanId, command.FeatureId, command.Limit);

            return Result.Success();
        }
    }
}
