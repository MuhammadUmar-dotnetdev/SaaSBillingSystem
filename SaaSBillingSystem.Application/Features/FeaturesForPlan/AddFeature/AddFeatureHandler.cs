using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.FeaturesForPlan.AddFeature
{
    public class AddFeatureHandler: IRequestHandler<AddFeatureCommand, Result<Guid>>
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly ILogger<AddFeatureHandler> _logger;

        public AddFeatureHandler(IFeatureRepository featureRepository, ILogger<AddFeatureHandler> logger)
        {
            _featureRepository = featureRepository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Handle(AddFeatureCommand command, CancellationToken cancellationToken)
        {
            var featureExists = await _featureRepository.ExistsByKeyAsync(command.Key);
            if (featureExists)
            {
                _logger.LogWarning("Feature with key {Key} already exists", command.Key);
                return Result<Guid>.Failure("Feature with given key already exists");
            }

            var feature = Feature.Create(command.Key, command.Name, command.Description);
            await _featureRepository.AddAsync(feature);
            _logger.LogInformation("Feature with key {Key} successfully created", command.Key);
            return Result<Guid>.Success(feature.Id);
        }
    }
}
