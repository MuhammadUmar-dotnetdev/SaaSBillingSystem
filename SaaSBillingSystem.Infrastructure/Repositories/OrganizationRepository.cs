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
        public async Task<Organization?> GetByNameAsync(string name)
        {
            return await _context.Organizations.FirstOrDefaultAsync(o => o.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Organization>> GetAllAsync()
        {
            return await _context.Organizations.ToListAsync();
        }

        public async Task<string?> GetNameByIdAsync(Guid id)
        {
            return await _context.Organizations.Where(o => o.Id == id).Select(o => o.Name).FirstOrDefaultAsync();
        }

        public async Task<List<Organization>> GetByIdsAsync(List<Guid> ids)
        {
            if(ids == null || ids.Count == 0)
            {
                return new List<Organization>();
            }
            return await _context.Organizations.Where(o => ids.Contains(o.Id)).ToListAsync();
        }

        public async Task<Organization?> GetByIdAsync(Guid id)
        {
            return await _context.Organizations.FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
