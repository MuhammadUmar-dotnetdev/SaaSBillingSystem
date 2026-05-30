using MediatR;

namespace SaaSBillingSystem.Application.Features.Organizations.GetAllUsersOfOrganizationById
{
    public class GetAllUsersOfOrganizationByIdCommand: IRequest<List<GetAllUsersOfOrganizationByIdResponse>>
    {
        public Guid Id { get; set; }

        public GetAllUsersOfOrganizationByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
