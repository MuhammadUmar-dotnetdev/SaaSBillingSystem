using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Organizations.GetAllOrganizations;
using SaaSBillingSystem.Application.Features.Organizations.GetAllUsersOfOrganizationById;
using SaaSBillingSystem.Application.Features.Organizations.GetAllUsersOfOrganizationByName;

namespace SaaSBillingSystem.API.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
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

        [HttpGet("users/{name}")]
        public async Task<IActionResult> GetAllUsersOfOrganizationByName(string name)
        {
            var list = await _mediator.Send(new GetAllUsersOfOrganizationByNameCommand(name));
            return Ok(list);
        }
        [HttpGet("users/{id:guid}")]
        public async Task<IActionResult> GetAllUsersOfOrganizationById(Guid id)
        {
            var list = await _mediator.Send(new GetAllUsersOfOrganizationByIdCommand(id));
            return Ok(list);
        }
    }
}
