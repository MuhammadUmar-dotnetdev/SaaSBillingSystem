using MediatR;
using SaaSBillingSystem.Application.Interfaces;

namespace SaaSBillingSystem.Application.Features.Organizations.GetAllUsersOfOrganizationById
{
    public class GetAllUsersOfOrganizationByIdHandler: IRequestHandler<GetAllUsersOfOrganizationByIdCommand, List<GetAllUsersOfOrganizationByIdResponse>>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetAllUsersOfOrganizationByIdHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        public async Task<List<GetAllUsersOfOrganizationByIdResponse>> Handle(GetAllUsersOfOrganizationByIdCommand command, CancellationToken cancellationToken)
        {
            var listOfUsers = await _organizationRepository.GetUsersOfOrganizationById(command.Id);
            return listOfUsers.Select(lu => new GetAllUsersOfOrganizationByIdResponse
            {
                Id = lu.Id,
                Email = lu.Email
            }).ToList();
        }
    }
}
