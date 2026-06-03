namespace SaaSBillingSystem.Application.Features.Login
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<UserOrganizationDTO> UserOrganizations { get; set; } = new List<UserOrganizationDTO>();
    }
}
