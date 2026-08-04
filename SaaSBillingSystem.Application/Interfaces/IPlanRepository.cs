using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IPlanRepository
    {
        Task<Guid> AddAsync(Plan plan);
        Task<Plan?> GetPlanByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Plan>> GetByIdsAsync(List<Guid> ids);
        Task<List<Plan>> GetAllPlansAsync();
        Task UpdateAsync(Plan plan, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsByNameAndBillingCycleAsync(string name, BillingCycle billingCycle, CancellationToken cancellationToken);
    }
}
