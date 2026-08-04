using SaaSBillingSystem.Application.DTOs;
using SaaSBillingSystem.Application.Interfaces;
using System.ComponentModel;

namespace SaaSBillingSystem.Infrastructure.Services
{
    public class ChatService: IChatService
    {
        private readonly IInvitationRepository _invitationRepository;
        public ChatService(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }
        [Description("Calculates total price including sales tax given item amount.")]
        public int CalculateTotalTax([Description("Total money entered")] int money)
        {
            var tax = 10;
            return money + tax;
        }
        [Description("Checks whether the customer have any invitations by owners of organizations in a system")]
        public async Task<List<InvitationDTO>> CheckInvitationsList([Description("Email of customer")] string userEmail)
        {
            var fetchedList = await _invitationRepository.GetInvitationsByEmailAsync(userEmail);

            var invitationsList = fetchedList.Select(i => new InvitationDTO
            {
                Email = i.Email,
                Role = i.Role.ToString(),
                Status = i.Status.ToString(),
                ExpiresAtUtc = i.ExpiresAtUtc,
                CreatedAtUtc = i.CreatedAtUtc,
                AcceptedAtUtc = i.AcceptedAtUtc,
                RevokedAtUtc = i.RevokedAtUtc
            }).ToList();
            return invitationsList;
        }

        //[Description("Checks which invitations for the customer are accepted not accepted")]
        //public int CheckInvitationsStatus([Description("Email of customer")] string userEmail)
        //{
        //    return 1;
        //}


    }
}
