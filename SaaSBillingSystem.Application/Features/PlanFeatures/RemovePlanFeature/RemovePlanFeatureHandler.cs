using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.RemovePlanFeature
{
    public class RemovePlanFeatureHandler: IRequestHandler<RemovePlanFeatureCommand, Result>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        private readonly ILogger<RemovePlanFeatureHandler> _logger;
        public RemovePlanFeatureHandler(IPlanFeatureRepository planFeatureRepository, ILogger<RemovePlanFeatureHandler> logger)
        {
            _planFeatureRepository = planFeatureRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(RemovePlanFeatureCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdsAsync(command.PlanId, command.FeatureId, cancellationToken);
            if(planFeature == null)
            {
                _logger.LogWarning("PlanFeature with PlanId {PlanId} and FeatureId {FeatureId} is not found", command.PlanId, command.FeatureId);
                return Result.Failure("PlanFeature with given PlanId and FeatureId Is Not Found");
            }
            await _planFeatureRepository.RemoveAsync(planFeature, cancellationToken);
            _logger.LogInformation("PlanFeature with PlanId {PlanId} and FeatureId {FeatureId} is successfully removed", command.PlanId, command.FeatureId);

            return Result.Success();
        }
    }
}
