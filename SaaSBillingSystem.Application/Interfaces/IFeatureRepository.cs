using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IFeatureRepository
    {
        Task AddAsync(Feature feature);

        Task<Feature?> GetByIdAsync(Guid id);

        Task<List<Feature>> GetAllAsync();

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        Task<Feature?> GetByKeyAsync(string key);
        Task<bool> ExistsByKeyAsync(string key);

        Task UpdateAsync(Feature feature);
    }
}
