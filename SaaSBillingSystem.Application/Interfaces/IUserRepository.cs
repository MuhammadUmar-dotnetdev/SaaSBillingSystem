using SaaSBillingSystem.Application.Features.Users.UserDtos;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllUsersAsync();
}