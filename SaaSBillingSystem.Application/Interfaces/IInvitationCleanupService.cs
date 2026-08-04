namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IInvitationCleanupService
    {
        Task DeletePendingInvitationsAsync(Guid organizationId, string email, CancellationToken cancellationToken);
    }
}
