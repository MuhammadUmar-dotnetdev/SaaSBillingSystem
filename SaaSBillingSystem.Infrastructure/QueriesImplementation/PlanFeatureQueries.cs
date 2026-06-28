using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Features.PlanFeatures.GetFeaturesForPlan;
using SaaSBillingSystem.Application.Features.PlanFeatures.GetPlansForFeature;
using SaaSBillingSystem.Application.Queries;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.QueriesImplementation
{
    public class PlanFeatureQueries : IPlanFeatureQueries
    {
        private readonly ApplicationDbContext _context;
        public PlanFeatureQueries(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetPlansForFeatureResponse>> GetPlansForFeatureAsync(Guid featureId, CancellationToken cancellationToken)
        {
            return await _context.PlanFeatures
                .Where(x => x.FeatureId == featureId)
                .Select(x => new GetPlansForFeatureResponse
                {
                    PlanId = x.PlanId,
                    PlanName = x.Plan.Name,
                    PlanDescription = x.Plan.Description,
                    IsEnabled = x.IsEnabled,
                    Limit = x.Limit,
                    Unit = x.Unit
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<List<GetFeaturesForPlanResponse>> GetFeaturesForPlanAsync(Guid planId, CancellationToken cancellationToken)
        {
            return await _context.PlanFeatures
                .Where(pf => pf.PlanId == planId)
                .Select(pf => new GetFeaturesForPlanResponse
                {
                    FeatureId = pf.FeatureId,
                    Key = pf.Feature.Key,
                    Name = pf.Feature.Name,
                    Description = pf.Feature.Description,
                    IsEnabled = pf.IsEnabled,
                    Limit = pf.Limit,
                    Unit = pf.Unit
                })
                .ToListAsync(cancellationToken);
        }
    }
}
