using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<List<Organization>> GetAllAsync();
        Task AddAsync(Organization organization);
        Task<bool> ExistsByNameAsync(string name);
        Task<List<User>> GetUsersOfOrganizationByName(string name);
        Task<List<User>> GetUsersOfOrganizationById(Guid Id);
    }
}
