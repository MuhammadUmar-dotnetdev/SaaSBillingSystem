using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.FeaturesForPlan.RenameFeature
{
    public class RenameFeatureHandler: IRequestHandler<RenameFeatureCommand, Result>
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly ILogger<RenameFeatureHandler> _logger;

        public RenameFeatureHandler(IFeatureRepository featureRepository, ILogger<RenameFeatureHandler> logger)
        {
            _featureRepository = featureRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(RenameFeatureCommand command, CancellationToken cancellationToken)
        {
            var feature = await _featureRepository.GetByIdAsync(command.Id);
            if (feature == null)
            {
                _logger.LogWarning("Feature with id {Id} not found", command.Id);
                return Result.Failure("Feature with given id not found");
            }
            feature.Rename(command.Name, command.Description);
            await _featureRepository.UpdateAsync(feature);
            _logger.LogInformation("Feature with id {Id} successfully renamed", command.Id);
            return Result.Success();
        }
    }
}
