using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.Repositories
{
    public class SubscriptionRepository: ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }
        public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriptionId);
        }

        public async Task<List<Subscription>> GetByIdsAsync(List<Guid> ids)
        {
            return await _context.Subscriptions.Where(s => ids.Contains(s.Id)).ToListAsync();
        }
        public async Task<bool> ExistsAsync(Guid organizationId, Guid planId)
        {
            return await _context.Subscriptions.AnyAsync(s => s.OrganizationId == organizationId && s.PlanId == planId);
        }

        public async Task<Subscription?> GetByOrganization(Guid organizationId)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(s => s.OrganizationId == organizationId);
        }

        public async Task UpdateAsync(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }
    }
}
