using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Subscriptions.ActivateSubscription;
using SaaSBillingSystem.Application.Features.Subscriptions.CancelSubscription;
using SaaSBillingSystem.Application.Features.Subscriptions.CancelSubscriptionAtEndOfPeriod;
using SaaSBillingSystem.Application.Features.Subscriptions.CreateSubscription;
using SaaSBillingSystem.Application.Features.Subscriptions.DowngradePlan;
using SaaSBillingSystem.Application.Features.Subscriptions.ExpireSubscription;
using SaaSBillingSystem.Application.Features.Subscriptions.IsSubscriptionActive;
using SaaSBillingSystem.Application.Features.Subscriptions.IsSubscriptionExpired;
using SaaSBillingSystem.Application.Features.Subscriptions.IsSubscriptionInTrial;
using SaaSBillingSystem.Application.Features.Subscriptions.MarkPastDueSubscription;
using SaaSBillingSystem.Application.Features.Subscriptions.RenewSubscription;
using SaaSBillingSystem.Application.Features.Subscriptions.ResumeSubscription;
using SaaSBillingSystem.Application.Features.Subscriptions.SuspendSubscription;
using SaaSBillingSystem.Application.Features.Subscriptions.UpgradePlan;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        [EndpointSummary("Create Subscription")]
        [EndpointDescription("This endpoint creates new subscription in a system")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [EndpointSummary("Activate Subscription")]
        [EndpointDescription("This endpoint avtivates existing subscription in a system")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [EndpointSummary("Suspend Subscription")]
        [EndpointDescription("This endpoint suspend existing subscription in a system")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [EndpointSummary("Expire Subscription")]
        [EndpointDescription("This endpoint expires existing subscription in a system")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("expire/{id:guid}")]
        public async Task<IActionResult> ExpireAsync(Guid id)
        {
            var result = await _mediator.Send(new ExpireSubscriptionCommand(id));
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Cancel Subscription")]
        [EndpointDescription("This endpoint cancels existing subscription in a system")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("cancel/{id:guid}")]
        public async Task<IActionResult> CancelAsync(Guid id)
        {
            var result = await _mediator.Send(new CancelSubscriptionCommand(id));
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Mark Subscription Past Due")]
        [EndpointDescription("This endpoint marks existing subscription as past due in a system")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("mark-past-due/{id:guid}")]
        public async Task<IActionResult> MarkPastDueAsync(Guid id)
        {
            var result = await _mediator.Send(new MarkPastDueSubscriptionCommand(id));
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Upgrade Subscription")]
        [EndpointDescription("This endpoint upgrades existing subscription in a system by assigning it new plan")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("upgrade")]
        public async Task<IActionResult> UpgradePlanAsync(UpgradePlanCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Downgrade Subscription")]
        [EndpointDescription("This endpoint downgrades existing subscription in a system by assigning it new plan")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("downgrade")]
        public async Task<IActionResult> UpgradePlanAsync(DowngradePlanCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Renew Subscription")]
        [EndpointDescription("This endpoint renews existing subscription in a system")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("renew")]
        public async Task<IActionResult> RenewAsync(RenewSubscriptionCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Is Subscription Active")]
        [EndpointDescription("This endpoint checks if existing subscription in a system is active or not")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("is-active/{id:guid}")]
        public async Task<IActionResult> IsActiveAsync(Guid id)
        {
            var result = await _mediator.Send(new IsSubscriptionActiveCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Is Subscription Expired")]
        [EndpointDescription("This endpoint checks if existing subscription in a system is expired or not")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("is-expired/{id:guid}")]
        public async Task<IActionResult> IsExpiredAsync(Guid id)
        {
            var result = await _mediator.Send(new IsSubscriptionExpiredCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Is Subscription In Trial")]
        [EndpointDescription("This endpoint checks if existing subscription in a system is in trial or not")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("is-in-trial/{id:guid}")]
        public async Task<IActionResult> IsInTrialAsync(Guid id)
        {
            var result = await _mediator.Send(new IsSubscriptionInTrialCommand(id));
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Resume Subscription")]
        [EndpointDescription("This endpoint resumes existing subscription in a system")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("resume/{id:guid}")]
        public async Task<IActionResult> ResumeAsync(Guid id)
        {
            var result = await _mediator.Send(new ResumeSubscriptionCommand(id));
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Cancel Subscription At The End Of Period")]
        [EndpointDescription("This endpoint cancels existing subscription in a system when set end period ends")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("cancel-at-end-of-period/{id:guid}")]
        public async Task<IActionResult> CancelAtEndOfPeriodAsync(Guid id)
        {
            var result = await _mediator.Send(new CancelSubscriptionAtEndOfPeriodCommand(id));
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }
    }
}
