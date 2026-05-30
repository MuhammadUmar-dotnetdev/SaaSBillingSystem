namespace SaaSBillingSystem.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(Domain.Entities.User user);
}