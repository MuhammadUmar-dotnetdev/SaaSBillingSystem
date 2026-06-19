using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);

        Task<Payment?> GetByIdAsync(Guid id);

        Task<List<Payment>> GetByInvoiceIdAsync(Guid invoiceId);

        Task<List<Payment>> GetAllAsync();

        Task UpdateAsync(Payment payment);

        Task<bool> HasSuccessfulPaymentAsync(Guid invoiceId);
    }
}
