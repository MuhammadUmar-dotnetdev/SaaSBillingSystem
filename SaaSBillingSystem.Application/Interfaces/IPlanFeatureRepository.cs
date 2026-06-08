using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IPlanFeatureRepository
    {
        Task AddAsync(PlanFeature planFeature);
        Task<PlanFeature?> GetByIdAsync(Guid id);
        Task<List<PlanFeature>> GetAllAsync();
        Task UpdateAsync(PlanFeature planFeature);
        Task<bool> ExistsAsync(Guid planId, string key);
    }
}
