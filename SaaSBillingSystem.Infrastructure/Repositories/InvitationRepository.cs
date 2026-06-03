using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.Repositories
{
    public class InvitationRepository: IInvitationRepository
    {
        private readonly ApplicationDbContext _context;
        public InvitationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Invitation invitation)
        {
            await _context.Invitations.AddAsync(invitation);
            await _context.SaveChangesAsync();
        }

        public async Task<Invitation?> GetByIdAsync(Guid id)
        {
            return await _context.Invitations.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Invitation?> GetByTokenAsync(string token)
        {
            return await _context.Invitations.FirstOrDefaultAsync(i => i.Token == token);
        }

        public async Task<List<Invitation>> GetByOrganizationIdAsync(Guid organizationId)
        {
            return await _context.Invitations.Where(i => i.OrganizationId == organizationId).ToListAsync();
        }

        public async Task<bool> ExistsPendingInvitationAsync(Guid organizationId, string email)
        {
            return await _context.Invitations.AnyAsync(i => i.OrganizationId == organizationId && i.Email == email && i.Status == InvitationStatus.Pending && i.ExpiresAtUtc > DateTime.UtcNow);
        }

        public async Task UpdateAsync(Invitation invitation)
        {
            _context.Invitations.Update(invitation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Invitation invitation)
        {
            _context.Invitations.Remove(invitation);
            await _context.SaveChangesAsync();
        }
    }
}
