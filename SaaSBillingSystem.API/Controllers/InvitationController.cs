using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Invitations.AcceptInvitation;
using SaaSBillingSystem.Application.Features.Invitations.AddInvitation;

namespace SaaSBillingSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationController: ControllerBase
    {
        private readonly IMediator _mediator;
        public InvitationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EndpointSummary("Create Invitation")]
        [EndpointDescription("This endpoint creates new invitation in a system")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(CreateInvitationCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptAsync(AcceptInvitationCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
