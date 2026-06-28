using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Domain.Entities;

public class Feature
{
    public Guid Id { get; private set; }
    public ICollection<PlanFeature> PlanFeatures { get; private set; } = null!;
    public string Key { get; private set; } = null!;

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    private Feature() { }

    public static Feature Create(
        string key,
        string name,
        string description)
    {
        return new Feature
        {
            Id = Guid.NewGuid(),

            Key = key,
            Name = name,
            Description = description,

            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow
        };
    }

    public Result Rename(string name, string description)
    {
        if(Name == name || Description == description)
        {
            return Result.Failure("Name or description have already same value");
        }
        Name = name;
        Description = description;

        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }
}