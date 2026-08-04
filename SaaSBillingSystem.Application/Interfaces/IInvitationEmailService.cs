using SaaSBillingSystem.Domain.Entities;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IInvitationEmailService
    {
        Task SendInvitationAsync(Invitation invitation, string organizationName, CancellationToken cancellationToken);
    }
}
