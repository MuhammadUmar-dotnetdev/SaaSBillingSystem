using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.Services
{
    public class InvitationCleanupService : IInvitationCleanupService
    {
        private readonly ApplicationDbContext _context;
        public InvitationCleanupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task DeletePendingInvitationsAsync(Guid organizationId, string email, CancellationToken cancellationToken)
        {
            var invitations = await _context.Invitations.Where(i => i.OrganizationId == organizationId && i.Email == email && i.Status == InvitationStatus.Pending).ToListAsync();

            if(invitations.Count == 0)
            {
                return;
            }

            _context.Invitations.RemoveRange(invitations);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
