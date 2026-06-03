using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IInvitationRepository
    {
        Task AddAsync(Invitation invitation);

        Task<Invitation?> GetByIdAsync(Guid id);

        Task<Invitation?> GetByTokenAsync(string token);

        Task<List<Invitation>> GetByOrganizationIdAsync(
            Guid organizationId);

        Task<bool> ExistsPendingInvitationAsync(
            Guid organizationId,
            string email);

        Task UpdateAsync(Invitation invitation);

        Task DeleteAsync(Invitation invitation);
    }
}
