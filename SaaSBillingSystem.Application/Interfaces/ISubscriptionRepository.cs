using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task AddAsync(Subscription subscription);
        Task<Subscription?> GetByIdAsync(Guid subscriptionId);
        Task<List<Subscription>> GetByIdsAsync(List<Guid> ids);
        Task<bool> ExistsAsync(Guid organizationId, Guid planId);
        Task<Subscription?> GetByOrganization(Guid organizationId);
        Task UpdateAsync(Subscription subscription);
    }
}
