using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Domain.Enums;
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

        public async Task<bool> ExistsAsync(Guid userId, Guid organizationId)
        {
            return await _context.OrganizationMemberships.AnyAsync(om => om.UserId == userId && om.OrganizationId == organizationId);
        }

        public async Task<List<OrganizationRole>> GetRoleAsync(Guid userId)
        {
            return await _context.OrganizationMemberships.Where(om => om.UserId == userId).Select(om => om.Role).ToListAsync();
        }

        public async Task<OrganizationMembership?> GetAsync(Guid userId, Guid organizationId)
        {
            return await _context.OrganizationMemberships.FirstOrDefaultAsync(om => om.UserId == userId && om.OrganizationId == organizationId);
        }

        public async Task<List<OrganizationMembership>> GetByUserIdAsync(Guid userId)
        {
            return await _context.OrganizationMemberships.Where(om => om.UserId == userId).ToListAsync();
        }
    }
}
