using MediatR;

namespace SaaSBillingSystem.Application.Features.Organizations.GetAllUsersOfOrganizationByName
{
    public class GetAllUsersOfOrganizationByNameCommand: IRequest<List<GetAllUsersOfOrganizationByNameResponse>>
    {
        public string Name { get; set; } = string.Empty;

        public GetAllUsersOfOrganizationByNameCommand(string name)
        {
            this.Name = name;
        }
    }
}
