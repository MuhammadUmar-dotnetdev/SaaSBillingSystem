using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Application.Features.Login
{
    public class UserOrganizationDTO
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public OrganizationRole organizationRole { get; set; }
    }
}
