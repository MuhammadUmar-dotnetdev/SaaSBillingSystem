namespace SaaSBillingSystem.Domain.Entities;

public class Feature
{
    public Guid Id { get; private set; }
    public ICollection<PlanFeature> PlanFeatures { get; private set; } = null!;
    public string Key { get; private set; } = null!;

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public bool IsEnabled { get; private set; }
    public int? Limit { get; private set; }
    public string? Unit { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    private Feature() { }

    public static Feature Create(
        string key,
        string name,
        string description,
        bool isEnabled = true,
        int? limit = null,
        string? unit = null)
    {
        return new Feature
        {
            Id = Guid.NewGuid(),

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