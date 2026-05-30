using MediatR;
using SaaSBillingSystem.Application.Interfaces;

namespace SaaSBillingSystem.Application.Features.Organizations.GetAllUsersOfOrganizationByName
{
    public class GetAllUsersOfOrganizationByNameHandler: IRequestHandler<GetAllUsersOfOrganizationByNameCommand, List<GetAllUsersOfOrganizationByNameResponse>>
    {
        private readonly IOrganizationRepository _organizationRepository;
        public GetAllUsersOfOrganizationByNameHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<List<GetAllUsersOfOrganizationByNameResponse>> Handle(GetAllUsersOfOrganizationByNameCommand command, CancellationToken cancellationToken)
        {
            var listOfUsers = await _organizationRepository.GetUsersOfOrganizationByName(command.Name);
            return listOfUsers.Select(lu => new GetAllUsersOfOrganizationByNameResponse
            {
                Id = lu.Id,
                Email = lu.Email
            }).ToList();
        }
    }
}
