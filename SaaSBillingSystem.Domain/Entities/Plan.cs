using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

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
    public ICollection<PlanFeature> PlanFeatures { get; private set; } = new List<PlanFeature>();

    public ICollection<Subscription> Subscriptions { get; private set; } = new List<Subscription>();

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

    public void Update(string name,
        string description,
        decimal price,
        BillingCycle billingCycle,
        int maxUsers,
        int maxProjects,
        long maxStorageInMb,
        bool isPublic = true)
    {
        Name = name;
        Description = description;

        Price = price;
        BillingCycle = billingCycle;

        MaxUsers = maxUsers;
        MaxProjects = maxProjects;
        MaxStorageInMb = maxStorageInMb;

        IsPublic = isPublic;
    }

    public Result UpdatePricing(decimal newPrice)
    {
        if (newPrice < 0)
        {
            return Result.Failure("Price cannot be negative.");
        }

        Price = newPrice;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result ChangeLimits(int maxUsers, int maxProjects, long maxStorageInMb)
    {
        if (MaxUsers == maxUsers)
        {
            return Result.Failure("This Plan's maxUsers is already set to given value");
        }
        MaxUsers = maxUsers;

        if (MaxProjects == maxProjects)
        {
            return Result.Failure("This Plan's maxProjects is already set to given value");
        }
        MaxProjects = maxProjects;

        if (MaxStorageInMb == maxStorageInMb)
        {
            return Result.Failure("This Plan's maxStorageInMb is already set to given value");
        }
        MaxStorageInMb = maxStorageInMb;

        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result Rename(string name, string description)
    {
        if (Name == name)
        {
            return Result.Failure("This Plan's name is already set to given name");
        }
        Name = name;

        if (Description == description)
        {
            return Result.Failure("This Plan's description is already set to given value");
        }

        Description = description;

        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive == true)
        {
            return Result.Failure("This plan is already activated");
        }
        IsActive = true;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result Deactivate()
    {
        if (IsActive == false)
        {
            return Result.Failure("This plan is already deactivated");
        }
        IsActive = false;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result MakePrivate()
    {
        if (IsPublic == false)
        {
            return Result.Failure("This plan is already set to private");
        }
        IsPublic = false;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    public Result MakePublic()
    {
        if (IsPublic == true)
        {
            return Result.Failure("This plan is already set to public");
        }
        IsPublic = true;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }
}