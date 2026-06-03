using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Auth.RegisterOwner
{
    public class RegisterOwnerCommand: IRequest<Result<RegisterOwnerResponse>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
    }
}
