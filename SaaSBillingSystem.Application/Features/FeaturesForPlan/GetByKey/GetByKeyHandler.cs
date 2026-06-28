using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.FeaturesForPlan.GetByKey
{
    public class GetByKeyHandler: IRequestHandler<GetByKeyCommand, Result<GetByKeyResponse>>
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly ILogger<GetByKeyHandler> _logger;
        public GetByKeyHandler(IFeatureRepository featureRepository, ILogger<GetByKeyHandler> logger)
        {
            _featureRepository = featureRepository;
            _logger = logger;
        }

        public async Task<Result<GetByKeyResponse>> Handle(GetByKeyCommand command, CancellationToken cancellationToken)
        {
            var feature = await _featureRepository.GetByKeyAsync(command.Key);
            if (feature == null)
            {
                _logger.LogWarning("Feature with key {Key} not found", command.Key);
                return Result<GetByKeyResponse>.Failure("Feature with given key not found");
            }

            var response = new GetByKeyResponse(feature.Id, feature.Key, feature.Name, feature.Description);
            return Result<GetByKeyResponse>.Success(response);
        }
    }
}
