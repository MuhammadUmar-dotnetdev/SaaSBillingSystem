using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IOrganizationMembershipRepository
    {
        Task AddAsync(OrganizationMembership organizationMembership);
    }
}
