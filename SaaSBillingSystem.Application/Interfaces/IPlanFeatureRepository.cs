using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IPlanFeatureRepository
    {
        Task AddAsync(Feature planFeature);
        Task<Feature?> GetByIdAsync(Guid id);
        Task<List<Feature>> GetAllAsync();
        Task UpdateAsync(Feature planFeature);
        Task<bool> ExistsAsync(Guid planId, string key);
    }
}
