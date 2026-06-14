using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task<Invoice?> GetByIdAsync(Guid id);
        Task<List<Invoice>> GetAllAsync();
        Task<List<Invoice>> GetByOrganizationAsync(Guid organizationId);
        Task<List<Invoice>> GetBySubscriptionAsync(Guid subscriptionId);
        Task<bool> ExistsForPeriodAsync(Guid subscriptionId, DateTime periodStart, DateTime periodEnd);
    }
}
