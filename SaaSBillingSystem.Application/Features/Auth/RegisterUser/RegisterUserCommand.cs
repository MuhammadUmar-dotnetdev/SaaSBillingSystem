using MediatR;

namespace SaaSBillingSystem.Application.Features.Auth.RegisterUser;

public class RegisterUserCommand : IRequest<RegisterUserResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}