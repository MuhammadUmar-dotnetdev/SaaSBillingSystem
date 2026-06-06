using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invitations.AcceptInvitation
{
    public class AcceptInvitationCommand: IRequest<Result<AcceptInvitationResponse>>
    {
        public string Token { get; set; } = string.Empty;
    }
}
