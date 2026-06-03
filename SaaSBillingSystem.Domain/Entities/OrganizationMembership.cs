using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Domain.Entities
{
    public class OrganizationMembership
    {
        public Guid UserId { get; private set; }

        public Guid OrganizationId { get; private set; }

        public OrganizationRole Role { get; private set; }

        public User User { get; private set; } = null!;

        public Organization Organization { get; private set; } = null!;

        private OrganizationMembership() { }

        public OrganizationMembership(Guid userId, Guid orgId, OrganizationRole role)
        {
            UserId = userId;
            OrganizationId = orgId;
            Role = role;
        }
    }
}
