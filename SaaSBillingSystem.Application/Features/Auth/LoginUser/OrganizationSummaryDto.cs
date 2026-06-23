using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Application.Features.Auth.LoginUser
{
    public class OrganizationSummaryDto
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public OrganizationRole Role { get; set; }
    }
}
