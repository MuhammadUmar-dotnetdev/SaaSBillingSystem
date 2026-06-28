using MediatR;
using Microsoft.Extensions.Logging;
using SaaSBillingSystem.Application.Queries;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.PlanFeatures.GetFeaturesForPlan
{
    public class GetFeaturesForPlanHandler: IRequestHandler<GetFeaturesForPlanCommand, Result<List<GetFeaturesForPlanResponse>>>
    {
        private readonly IPlanFeatureQueries _queries;
        private readonly ILogger<GetFeaturesForPlanHandler> _logger;
        public GetFeaturesForPlanHandler(IPlanFeatureQueries queries, ILogger<GetFeaturesForPlanHandler> logger)
        {
            _queries = queries;
            _logger = logger;
        }

        public async Task<Result<List<GetFeaturesForPlanResponse>>> Handle(GetFeaturesForPlanCommand command, CancellationToken cancellationToken)
        {
            var list = await _queries.GetFeaturesForPlanAsync(command.PlanId, cancellationToken);
            if (list.Count == 0)
            {
                _logger.LogWarning("No PlanFeature is found for {PlanId}", command.PlanId);
                return Result<List<GetFeaturesForPlanResponse>>.Failure("No PlanFeature is found for given PlanId");
            }
            return Result<List<GetFeaturesForPlanResponse>>.Success(list);
        }
    }
}
