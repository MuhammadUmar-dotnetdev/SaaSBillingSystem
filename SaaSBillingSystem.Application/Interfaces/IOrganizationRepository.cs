using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<List<Organization>> GetAllAsync();
        Task AddAsync(Organization organization);
        Task<bool> ExistsByNameAsync(string name);
        Task<Organization?> GetByNameAsync(string name);
        Task<string?> GetNameByIdAsync(Guid id);
        Task<Organization?> GetByIdAsync(Guid id);
        Task<List<Organization>> GetByIdsAsync(List<Guid> ids);
    }
}
