namespace SaaSBillingSystem.Domain.Entities;

public class PlanFeature
{
    public Guid Id { get; private set; }
    public Guid PlanId { get; private set; }
    public Plan Plan { get; private set; } = null!;
    public string Key { get; private set; } = null!;

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public bool IsEnabled { get; private set; }
    public int? Limit { get; private set; }
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