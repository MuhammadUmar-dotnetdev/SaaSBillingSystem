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

        public async Task<Guid> CreateAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
            return subscription.Id;
        }

        public async Task<Subscription?> GetByOrganization(Guid organizationId)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(s => s.OrganizationId == organizationId);
        }

        public async Task ActivateAsync(Guid subscriptionId)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriptionId);
            if(subscription != null)
            {
                subscription.Activate();
                await _context.SaveChangesAsync();
            }
        }

        public async Task SuspendAsync(Guid subscriptionId)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriptionId);
            if(subscription != null)
            {
                subscription.Suspend();
                await _context.SaveChangesAsync();
            }
        }
    }
}
