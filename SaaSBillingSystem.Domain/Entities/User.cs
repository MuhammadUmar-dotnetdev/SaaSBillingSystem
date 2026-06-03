using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public bool IsEmailVerified { get; private set; }

    public ICollection<OrganizationMembership> Memberships { get; private set; } = new List<OrganizationMembership>();
    // Constructor (important for domain control)
    public User(string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        IsEmailVerified = false;
    }

    // Domain behavior (business logic inside entity)
    public void VerifyEmail()
    {
        IsEmailVerified = true;
    }
}