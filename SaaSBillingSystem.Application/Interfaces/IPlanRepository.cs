using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IPlanRepository
    {
        Task<Guid> AddAsync(Plan plan);
        Task<Plan?> GetPlanByIdAsync(Guid id);
        Task<List<Plan>> GetByIdsAsync(List<Guid> ids);
        Task<List<Plan>> GetAllPlansAsync();
        Task UpdateAsync(Plan plan);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
