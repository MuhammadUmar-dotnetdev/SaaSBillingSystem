namespace SaaSBillingSystem.Application.Features.Organizations.GetAllOrganizations
{
    public class GetAllOrganizationsResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
