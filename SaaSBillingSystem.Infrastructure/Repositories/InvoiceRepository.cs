using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.Repositories
{
    public class InvoiceRepository: IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _context.Invoices.AsNoTracking().ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> ExistsForPeriodAsync(Guid subscriptionId, DateTime periodStart, DateTime periodEnd)
        {
            return await _context.Invoices.AnyAsync(i => i.SubscriptionId == subscriptionId && i.PeriodStartUtc == periodStart && i.PeriodEndUtc == periodEnd);
        }

        public async Task<List<Invoice>> GetByOrganizationAsync(Guid organizationId)
        {
            return await _context.Invoices.AsNoTracking().Where(i => i.OrganizationId == organizationId).ToListAsync();
        }

        public async Task<List<Invoice>> GetBySubscriptionAsync(Guid subscriptionId)
        {
            return await _context.Invoices.AsNoTracking().Where(i => i.SubscriptionId == subscriptionId).ToListAsync();
        }
    }
}
