using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Organizations.GetAllOrganizations;

namespace SaaSBillingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController: ControllerBase
    {
        private readonly IMediator _mediator;
        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EndpointSummary("This endpoint returns list of all organization")]
        [EndpointDescription("This api endpoint returns the list of all organizations currently existing in system")]
        [Produces<List<GetAllOrganizationsResponse>>()]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllOrganizationsCommand());
            return Ok(result);
        }
    }
}
