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

        [HttpPatch("markpastdue/{id:guid}")]
        public async Task<IActionResult> MarkPastDueAsync(Guid id)
        {
            var result = await _mediator.Send(new MarkPastDueSubscriptionCommand(id));
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }

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

        [HttpGet("isactive/{id:guid}")]
        public async Task<IActionResult> IsActiveAsync(Guid id)
        {
            var result = await _mediator.Send(new IsSubscriptionActiveCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        [HttpGet("isexpired/{id:guid}")]
        public async Task<IActionResult> IsExpiredAsync(Guid id)
        {
            var result = await _mediator.Send(new IsSubscriptionExpiredCommand(id));
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }

        [HttpGet("isintrial/{id:guid}")]
        public async Task<IActionResult> IsInTrialAsync(Guid id)
        {
            var result = await _mediator.Send(new IsSubscriptionInTrialCommand(id));
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }

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


        [HttpPatch("cancelatendofperiod/{id:guid}")]
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
