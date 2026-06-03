namespace SaaSBillingSystem.Domain.Entities;

public class Organization
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public ICollection<OrganizationMembership> Memberships { get; private set; } = new List<OrganizationMembership>();
    public ICollection<Invitation> Invitations { get; private set; } = new List<Invitation>();
    private Organization() { }

    public Organization(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }
}