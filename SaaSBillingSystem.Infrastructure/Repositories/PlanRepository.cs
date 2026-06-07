using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.Repositories
{
    public class PlanRepository: IPlanRepository
    {
        private readonly ApplicationDbContext _context;
        public PlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Plan plan)
        {
            await _context.Plans.AddAsync(plan);
            await _context.SaveChangesAsync();
            return plan.Id;
        }

        public async Task<Plan?> GetPlanByIdAsync(Guid id)
        {
            return await _context.Plans.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Plan>> GetAllPlansAsync()
        {
            return await _context.Plans.ToListAsync();
        }

        public async Task UpdateAsync(Plan plan)
        {
            _context.Plans.Update(plan);
            await _context.SaveChangesAsync();
        }
    }
}
