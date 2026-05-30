using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Application.Features.Users.UserDtos;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Infrastructure.Persistence;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        return user;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var list = await _context.Users.ToListAsync();
        return list;
    }
}