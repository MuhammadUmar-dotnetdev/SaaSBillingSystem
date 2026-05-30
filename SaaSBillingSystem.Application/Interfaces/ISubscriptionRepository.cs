using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Guid> CreateAsync(Subscription subscription);
        Task<Subscription?> GetByOrganization(Guid organizationId);
        Task ActivateAsync(Guid subscriptionId);
        Task SuspendAsync(Guid subscriptionId);
    }
}
