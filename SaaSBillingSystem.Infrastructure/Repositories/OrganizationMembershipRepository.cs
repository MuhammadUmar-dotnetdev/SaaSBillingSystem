using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.Repositories
{
    public class OrganizationMembershipRepository: IOrganizationMembershipRepository
    {
        private readonly ApplicationDbContext _context;
        public OrganizationMembershipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrganizationMembership organizationMembership)
        {
            await _context.OrganizationMemberships.AddAsync(organizationMembership);
            await _context.SaveChangesAsync();
        }
    }
}
