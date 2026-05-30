using MediatR;

namespace SaaSBillingSystem.Application.Features.Organizations.GetAllOrganizations
{
    public class GetAllOrganizationsCommand: IRequest<List<GetAllOrganizationsResponse>>
    {
    }
}
