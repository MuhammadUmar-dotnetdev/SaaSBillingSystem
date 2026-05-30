namespace SaaSBillingSystem.Domain.Entities;

public class Organization
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public ICollection<User> Users { get; private set; } = new List<User>();

    private Organization() { }

    public Organization(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }
}