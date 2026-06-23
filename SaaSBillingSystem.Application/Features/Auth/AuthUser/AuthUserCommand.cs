using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Auth.AuthUser
{
    public class AuthUserCommand: IRequest<Result<AuthUserResponse>>
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public Guid OrganizationId { get; set; }
    }
}
