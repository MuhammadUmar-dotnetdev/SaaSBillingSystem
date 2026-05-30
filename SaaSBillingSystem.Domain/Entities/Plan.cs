using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Domain.Entities;

public class Plan
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    // Pricing
    public decimal Price { get; private set; }

    // Monthly / Yearly
    public BillingCycle BillingCycle { get; private set; }

    // Limits
    public int MaxUsers { get; private set; }
    public int MaxProjects { get; private set; }
    public long MaxStorageInMb { get; private set; }

    // Visibility
    public bool IsActive { get; private set; }
    public bool IsPublic { get; private set; }

    // Navigation
    public ICollection<PlanFeature> Features { get; private set; }
        = new List<PlanFeature>();

    public ICollection<Subscription> Subscriptions { get; private set; }
        = new List<Subscription>();

    // Audit
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    private Plan() { }

    public static Plan Create(
        string name,
        string description,
        decimal price,
        BillingCycle billingCycle,
        int maxUsers,
        int maxProjects,
        long maxStorageInMb,
        bool isPublic = true)
    {
        return new Plan
        {
            Id = Guid.NewGuid(),

            Name = name,
            Description = description,

            Price = price,
            BillingCycle = billingCycle,

            MaxUsers = maxUsers,
            MaxProjects = maxProjects,
            MaxStorageInMb = maxStorageInMb,

            IsActive = true,
            IsPublic = isPublic,

            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow
        };
    }

    public void UpdatePricing(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Price cannot be negative.");

        Price = newPrice;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void ChangeLimits(
        int maxUsers,
        int maxProjects,
        long maxStorageInMb)
    {
        MaxUsers = maxUsers;
        MaxProjects = maxProjects;
        MaxStorageInMb = maxStorageInMb;

        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Rename(string name, string description)
    {
        Name = name;
        Description = description;

        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void MakePrivate()
    {
        IsPublic = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void MakePublic()
    {
        IsPublic = true;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void AddFeature(PlanFeature feature)
    {
        Features.Add(feature);
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void RemoveFeature(PlanFeature feature)
    {
        Features.Remove(feature);
        UpdatedAtUtc = DateTime.UtcNow;
    }
}