using SaaSBillingSystem.Application.DTOs;
using System.ComponentModel;

namespace SaaSBillingSystem.Application.Interfaces
{
    public interface IChatService
    {
        int CalculateTotalTax([Description("Total money entered")] int money);
        Task<List<InvitationDTO>> CheckInvitationsList(string userEmail);
    }
}
