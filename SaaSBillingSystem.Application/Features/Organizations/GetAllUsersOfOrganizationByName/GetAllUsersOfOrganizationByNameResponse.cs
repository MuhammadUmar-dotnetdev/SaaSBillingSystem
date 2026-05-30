namespace SaaSBillingSystem.Application.Features.Organizations.GetAllUsersOfOrganizationByName
{
    public class GetAllUsersOfOrganizationByNameResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
