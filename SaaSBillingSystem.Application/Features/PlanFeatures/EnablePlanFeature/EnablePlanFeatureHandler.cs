using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.EnablePlanFeature
{
    public class EnablePlanFeatureHandler: IRequestHandler<EnablePlanFeatureCommand, Result>
    {
        private readonly IPlanFeatureRepository _planFeatureRepository;
        private readonly ILogger<EnablePlanFeatureCommand> _logger;

        public EnablePlanFeatureHandler(IPlanFeatureRepository planFeatureRepository, ILogger<EnablePlanFeatureCommand> logger)
        {
            _planFeatureRepository = planFeatureRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(EnablePlanFeatureCommand command, CancellationToken cancellationToken)
        {
            var planFeature = await _planFeatureRepository.GetByIdsAsync(command.PlanId, command.FeatureId, cancellationToken);
            if (planFeature == null)
            {
                _logger.LogWarning("PlanFeature with PlanId {PlanId} and FeatureId {FeatureId} is not found", command.PlanId, command.FeatureId);
                return Result.Failure("PlanFeature with given PlanId and FeatureId Is Not Found");
            }
            planFeature.Enable();
            await _planFeatureRepository.UpdateAsync(planFeature, cancellationToken);
            _logger.LogInformation("PlanFeature with PlanId {PlanId} and FeatureId {FeatureId} is successfully enabled", command.PlanId, command.FeatureId);

            return Result.Success();
        }
    }
}
