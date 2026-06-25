using SaaSBillingSystem.Domain.Enums;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Domain.Entities;

public class Organization
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    public ICollection<OrganizationMembership> Memberships { get; private set; } = new List<OrganizationMembership>();
    public ICollection<Invitation> Invitations { get; private set; } = new List<Invitation>();
    private Organization() { }

    public Organization(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public Result Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure("Organization name cannot be empty.");
        }
           
        name = name.Trim();

        if (Name == name)
        {
            return Result.Failure("Organization already have given name");
        }

        Name = name;
        UpdatedAtUtc = DateTime.UtcNow;
        return Result.Success();
    }

    //public Result AddMember(Guid userId, OrganizationRole role)
    //{
    //    if(Memberships.Any(m => m.UserId == userId))
    //    {
    //        return Result.Failure("User already belongs to organization.");
    //    }

    //    Memberships.Add(new OrganizationMembership(userId, Id, role));
    //    UpdatedAtUtc = DateTime.UtcNow;
    //    return Result.Success();
    //}

    //public Result RemoveMember(Guid userId)
    //{
    //    var membership = Memberships.FirstOrDefault(x => x.UserId == userId);

    //    if (membership is null)
    //    {
    //        return Result.Failure("Membership not found.");
    //    }

    //    Memberships.Remove(membership);
    //    UpdatedAtUtc = DateTime.UtcNow;
    //    return Result.Success();
    //}
}