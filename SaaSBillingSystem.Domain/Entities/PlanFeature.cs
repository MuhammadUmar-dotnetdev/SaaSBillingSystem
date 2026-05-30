namespace SaaSBillingSystem.Domain.Entities;

public class PlanFeature
{
    public Guid Id { get; private set; }

    // FK to Plan
    public Guid PlanId { get; private set; }
    public Plan Plan { get; private set; } = null!;

    // Feature identifier (could also be a Feature table later)
    public string Key { get; private set; } = null!;
    // e.g. "AI_ACCESS", "API_CALLS", "ANALYTICS"

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    // Whether feature is enabled in this plan
    public bool IsEnabled { get; private set; }

    // Optional limit (null = unlimited)
    public int? Limit { get; private set; }

    // Unit of limit (requests, users, GB, etc.)
    public string? Unit { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    private PlanFeature() { }

    public static PlanFeature Create(
        Guid planId,
        string key,
        string name,
        string description,
        bool isEnabled = true,
        int? limit = null,
        string? unit = null)
    {
        return new PlanFeature
        {
            Id = Guid.NewGuid(),
            PlanId = planId,

            Key = key,
            Name = name,
            Description = description,

            IsEnabled = isEnabled,
            Limit = limit,
            Unit = unit,

            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow
        };
    }

    public void Enable()
    {
        IsEnabled = true;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Disable()
    {
        IsEnabled = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void UpdateLimit(int? limit, string? unit)
    {
        Limit = limit;
        Unit = unit;

        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Rename(string name, string description)
    {
        Name = name;
        Description = description;

        UpdatedAtUtc = DateTime.UtcNow;
    }
}