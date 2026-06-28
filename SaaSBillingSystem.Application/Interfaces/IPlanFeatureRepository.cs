using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IPlanFeatureRepository
    {
        Task AddAsync(PlanFeature planFeature);

        Task RemoveAsync(PlanFeature planFeature, CancellationToken cancellationToken);
        Task UpdateAsync(PlanFeature planFeature, CancellationToken cancellationToken);

        Task<PlanFeature?> GetByIdsAsync(Guid planId, Guid featureId, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid planId, Guid featureId, CancellationToken cancellationToken);
    }
}
