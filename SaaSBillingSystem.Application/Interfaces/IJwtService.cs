
using SaaSBillingSystem.Application.Features.Auth.LoginUser;
using SaaSBillingSystem.Shared.DTOs;

namespace SaaSBillingSystem.Application.Interfaces;

public interface IJwtService
{
    JwtDTO GenerateToken(AuthContext authContext);
}