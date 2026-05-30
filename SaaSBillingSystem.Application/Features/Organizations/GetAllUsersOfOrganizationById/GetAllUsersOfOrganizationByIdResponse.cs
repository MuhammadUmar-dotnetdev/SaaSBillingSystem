namespace SaaSBillingSystem.Application.Features.Organizations.GetAllUsersOfOrganizationById
{
    public class GetAllUsersOfOrganizationByIdResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
