using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly ApplicationDbContext _context;
        public FeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Feature feature)
        {
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByKeyAsync(string key)
        {
            return await _context.Features.AnyAsync(f => f.Key == key);
        }

        public async Task<Feature?> GetByKeyAsync(string key)
        {
            return await _context.Features.FirstOrDefaultAsync(f => f.Key == key);
        }

        Task<List<Feature>> IFeatureRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Feature?> GetByIdAsync(Guid id)
        {
            return await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task UpdateAsync(Feature feature)
        {
            _context.Update(feature);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Features.AnyAsync(f => f.Id == id, cancellationToken);
        }
    }
}
