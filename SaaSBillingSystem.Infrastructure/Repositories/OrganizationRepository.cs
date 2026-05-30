using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.Repositories
{
    public class OrganizationRepository: IOrganizationRepository
    {
        private readonly ApplicationDbContext _context;
        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Organization organization)
        {
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Organizations.AnyAsync(o => o.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Organization>> GetAllAsync()
        {
            return await _context.Organizations.ToListAsync();
        }

        public async Task<List<User>> GetUsersOfOrganizationByName(string name)
        {
            return await _context.Users.Where(u => EF.Functions.ILike(u.Organization.Name, name)).ToListAsync();
        }
        public async Task<List<User>> GetUsersOfOrganizationById(Guid id)
        {
            return await _context.Users.Where(u => u.OrganizationId == id).ToListAsync();
        }
    }
}
