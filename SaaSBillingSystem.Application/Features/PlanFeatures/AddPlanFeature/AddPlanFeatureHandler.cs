using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.AddPlanFeature
{
    public class AddPlanFeatureHandler: IRequestHandler<AddPlanFeatureCommand, Result>
    {
        private readonly ILogger<AddPlanFeatureHandler> _logger;
        private readonly IPlanFeatureRepository _planFeatureRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IFeatureRepository _featureRepository;
        public AddPlanFeatureHandler(ILogger<AddPlanFeatureHandler> logger, IPlanFeatureRepository planFeatureRepository, IPlanRepository planRepository, IFeatureRepository featureRepository)
        {
            _logger = logger;
            _planFeatureRepository = planFeatureRepository;
            _planRepository = planRepository;
            _featureRepository = featureRepository;
        }

        public async Task<Result> Handle(AddPlanFeatureCommand command, CancellationToken cancellationToken)
        {
            var planExists = await _planRepository.ExistsAsync(command.PlanId, cancellationToken);
            if (!planExists)
            {
                _logger.LogWarning("Plan with id {PlanId} is not found", command.PlanId);
                return Result.Failure("Plan with given id is not found");
            }

            var featureExists = await _featureRepository.ExistsAsync(command.FeatureId, cancellationToken);
            if (!featureExists)
            {
                _logger.LogWarning("Feature with id {FeatureId} is not found", command.FeatureId);
                return Result.Failure("Feature with given id is not found");
            }

            var planFeatureExists = await _planFeatureRepository.ExistsAsync(command.PlanId, command.FeatureId, cancellationToken);
            if (planFeatureExists)
            {
                _logger.LogWarning("Feature with id {FeatureId} is already assigned to Plan with {PlanId}", command.FeatureId, command.PlanId);
                return Result.Failure("The feature is already assigned to the selected plan.");
            }

            var planFeature = new PlanFeature(command.PlanId, command.FeatureId, command.IsEnabled, command.Limit, command.Unit);

            await _planFeatureRepository.AddAsync(planFeature);
            _logger.LogInformation("Feature with id {FeatureId} is successfully assigned to Plan with {PlanId}", command.FeatureId, command.PlanId);
            return Result.Success();
        }
    }
}
