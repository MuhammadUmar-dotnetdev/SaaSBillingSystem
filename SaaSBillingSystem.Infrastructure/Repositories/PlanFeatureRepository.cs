using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.Repositories
{
    public class PlanFeatureRepository: IPlanFeatureRepository
    {
        private readonly ApplicationDbContext _context;
        public PlanFeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PlanFeature planFeature)
        {
            await _context.PlanFeatures.AddAsync(planFeature);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid planId, Guid featureId, CancellationToken cancellationToken)
        {
            return await _context.PlanFeatures.AnyAsync(pf => pf.PlanId == planId && pf.FeatureId == featureId, cancellationToken);
        }

        public async Task RemoveAsync(PlanFeature planFeature, CancellationToken cancellationToken)
        {
            _context.Remove(planFeature);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(PlanFeature planFeature, CancellationToken cancellationToken)
        {
            _context.Update(planFeature);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PlanFeature?> GetByIdsAsync(Guid planId, Guid featureId, CancellationToken cancellationToken)
        {
            return await _context.PlanFeatures.FirstOrDefaultAsync(pf => pf.PlanId == planId && pf.FeatureId == featureId);
        }
    }
}
