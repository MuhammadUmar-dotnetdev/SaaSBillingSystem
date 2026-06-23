namespace SaaSBillingSystem.Application.Features.Auth.LoginUser
{
    public class LoginUserResponse
    {
        public string LoginToken { get; set; } = string.Empty;
        public List<OrganizationSummaryDto> Organizations { get; set; } = [];
    }
}
