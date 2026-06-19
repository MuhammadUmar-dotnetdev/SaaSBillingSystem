using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Infrastructure.Persistence;

namespace SaaSBillingSystem.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Payment payment)
        {
            await _context.AddAsync(payment);
            await _context.SaveChangesAsync();
        }
        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<Payment>> GetByInvoiceIdAsync(Guid invoiceId)
        {
            return await _context.Payments.Where(p => p.InvoiceId == invoiceId).ToListAsync();
        }
        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }
        public async Task UpdateAsync(Payment payment)
        {
            _context.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasSuccessfulPaymentAsync(Guid invoiceId)
        {
            return await _context.Payments.AnyAsync(p => p.InvoiceId == invoiceId);
        }
    }
}
