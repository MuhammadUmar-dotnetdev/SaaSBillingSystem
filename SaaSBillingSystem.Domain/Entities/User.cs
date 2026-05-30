using SaaSBillingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaaSBillingSystem.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public bool IsEmailVerified { get; private set; }

    public Guid OrganizationId { get; private set; }

    public Organization Organization { get; private set; } = null!;

    public UserRole Role { get; private set; }

    // Constructor (important for domain control)
    public User(string email, string passwordHash, Guid organizationId)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        IsEmailVerified = false;
        OrganizationId = organizationId;
        Role = UserRole.Member;
    }

    // Domain behavior (business logic inside entity)
    public void VerifyEmail()
    {
        IsEmailVerified = true;
    }
}