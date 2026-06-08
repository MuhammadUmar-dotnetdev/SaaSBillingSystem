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
            await _context.PlansFeatures.AddAsync(planFeature);
            await _context.SaveChangesAsync();
        }

        public async Task<PlanFeature?> GetByIdAsync(Guid id)
        {
            return await _context.PlansFeatures.FirstOrDefaultAsync(pf => pf.Id == id);
        }

        public async Task<List<PlanFeature>> GetAllAsync()
        {
            return await _context.PlansFeatures.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(PlanFeature planFeature)
        {
            _context.PlansFeatures.Update(planFeature);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid planId, string key)
        {
            return await _context.PlansFeatures.AnyAsync(pf => pf.PlanId == planId && pf.Key == key);
        }
    }
}
