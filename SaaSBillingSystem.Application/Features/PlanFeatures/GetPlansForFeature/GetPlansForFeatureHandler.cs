using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Queries;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetPlansForFeature
{
    public class GetPlansForFeatureHandler: IRequestHandler<GetPlansForFeatureCommand, Result<List<GetPlansForFeatureResponse>>>
    {
        private readonly IPlanFeatureQueries _queries;
        private readonly ILogger<GetPlansForFeatureHandler> _logger;
        public GetPlansForFeatureHandler(IPlanFeatureQueries queries, ILogger<GetPlansForFeatureHandler> logger)
        {
            _queries = queries;
            _logger = logger;
        }

        public async Task<Result<List<GetPlansForFeatureResponse>>> Handle(GetPlansForFeatureCommand command, CancellationToken cancellationToken)
        {
            var list = await _queries.GetPlansForFeatureAsync(command.FeatureId, cancellationToken);
            if (list.Count == 0)
            {
                _logger.LogWarning("No PlanFeature is found for {FeatureId}", command.FeatureId);
                return Result<List<GetPlansForFeatureResponse>>.Failure("No PlanFeature is found for given FeatureId");
            }
            return Result<List<GetPlansForFeatureResponse>>.Success(list);
        }
    }
}
