using MediatR;
using SaaSBillingSystem.Application.Interfaces;

namespace SaaSBillingSystem.Application.Features.Organizations.GetAllOrganizations
{
    internal class GetAllOrganizationsHandler: IRequestHandler<GetAllOrganizationsCommand, List<GetAllOrganizationsResponse>>
    {
        private readonly IOrganizationRepository _organizationRepository;
        public GetAllOrganizationsHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<List<GetAllOrganizationsResponse>> Handle(GetAllOrganizationsCommand command, CancellationToken cancellationToken)
        {
            var result = await _organizationRepository.GetAllAsync();
            return result.Select(o => new GetAllOrganizationsResponse
            {
                Id = o.Id,
                Name = o.Name,
                CreatedAtUtc = o.CreatedAtUtc,
            }).ToList();
        }
    }
}
