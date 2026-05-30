using MediatR;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Auth.LoginUser;

public class LoginUserCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}