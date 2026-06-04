using MediatR;
using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invitations.AddInvitation
{
    public class CreateInvitationCommand: IRequest<Result<CreateInvitationResponse>>
    {
        public Guid OrganizationId { get; set; }
        public string Email { get; set; } = string.Empty;
        public OrganizationRole Role { get; set; }
    }
}
