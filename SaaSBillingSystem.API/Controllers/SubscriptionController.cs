using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Subscriptions.ActivateSubscription;
using SaaSBillingSystem.Application.Features.Subscriptions.CreateSubscription;
using SaaSBillingSystem.Application.Features.Subscriptions.SuspendSubscription;

namespace SaaSBillingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController: ControllerBase
    {
        private readonly IMediator _mediator;
        public SubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateSubscriptionCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        [HttpPatch("activate/{id:guid}")]
        public async Task<IActionResult> ActivateAsync(Guid id)
        {
            var result = await _mediator.Send(new ActivateSubscriptionCommand(id));
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }

        [HttpPatch("suspend/{id:guid}")]
        public async Task<IActionResult> SuspendAsync(Guid id)
        {
            var result = await _mediator.Send(new SuspendSubscriptionCommand(id));
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }
    }
}
