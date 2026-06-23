using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IOrganizationMembershipRepository
    {
        Task AddAsync(OrganizationMembership organizationMembership);
        Task<bool> ExistsAsync(Guid userId, Guid organizationId);
        Task<OrganizationMembership?> GetAsync(Guid userId, Guid organizationId);
        Task<List<OrganizationRole>> GetRoleAsync(Guid userId);
        Task<List<OrganizationMembership>> GetByUserIdAsync(Guid userId);
    }
}
