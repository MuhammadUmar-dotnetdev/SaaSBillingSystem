using System;
using System.Collections.Generic;
using System.Text;

namespace SaaSBillingSystem.Application.Features.Auth.RegisterUser;

public class RegisterUserResponse
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
}