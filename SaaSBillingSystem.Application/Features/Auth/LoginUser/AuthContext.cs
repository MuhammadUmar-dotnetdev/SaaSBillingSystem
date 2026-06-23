using SaaSBillingSystem.Domain.Enums;

namespace SaaSBillingSystem.Application.Features.Auth.LoginUser
{
    public class AuthContext
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public OrganizationRole Role { get; set; }
    }
}
