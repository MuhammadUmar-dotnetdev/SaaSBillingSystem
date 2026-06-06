using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Invitations.AcceptInvitation;
using SaaSBillingSystem.Application.Features.Invitations.AddInvitation;

namespace SaaSBillingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationController: ControllerBase
    {
        private readonly IMediator _mediator;
        public InvitationController(IMediator mediator)
        {
            _mediator = mediator;
        }
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
