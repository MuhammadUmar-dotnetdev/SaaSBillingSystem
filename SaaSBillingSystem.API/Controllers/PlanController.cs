using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Plans.ActivatePlan;
using SaaSBillingSystem.Application.Features.Plans.ChangeLimits;
using SaaSBillingSystem.Application.Features.Plans.CreatePlan;
using SaaSBillingSystem.Application.Features.Plans.DeactivatePlan;
using SaaSBillingSystem.Application.Features.Plans.GetAllPlans;
using SaaSBillingSystem.Application.Features.Plans.GetPlanById;
using SaaSBillingSystem.Application.Features.Plans.MakePlanPrivate;
using SaaSBillingSystem.Application.Features.Plans.MakePlanPublic;
using SaaSBillingSystem.Application.Features.Plans.RenamePlan;
using SaaSBillingSystem.Application.Features.Plans.UpdatePlan;
using SaaSBillingSystem.Application.Features.Plans.UpdatePlanPricing;

namespace SaaSBillingSystem.API.Controllers
{
    //[Authorize(Policy = "User")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController: ControllerBase
    {
        private readonly IMediator _mediator;
        public PlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EndpointSummary("Create Plan")]
        [EndpointDescription("Creates a new plan in a system")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(CreatePlanCommand planCommand)
        {
            var response = await _mediator.Send(planCommand);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Error);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Value!.Id }, response.Value);
        }

        [EndpointSummary("Get Plan By Id")]
        [EndpointDescription("This endpoint takes plan id and return plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetPlanByIdCommand(id));
            
            if(!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Get All Plans")]
        [EndpointDescription("Get all plans from the system")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllPlansAsync()
        {
            var result = await _mediator.Send(new GetAllPlansCommand());
            if(!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Update Plan")]
        [EndpointDescription("This endpoint updates plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdatePlanCommand command)
        {
            var result = await _mediator.Send(command);
            if(!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Change Plan Limits")]
        [EndpointDescription("This endpoint updates limits of a plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("change-limits")]
        public async Task<IActionResult> ChangeLimitsAsync(ChangeLimitsCommand command)
        {
            var result = await _mediator.Send(command);
            if(!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Rename Plan")]
        [EndpointDescription("This endpoint updates name of a plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("rename")]
        public async Task<IActionResult> RenamePlanAsync(RenamePlanCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Activate Plan")]
        [EndpointDescription("This endpoint activates the plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("activate/{id:guid}")]
        public async Task<IActionResult> ActivatePlanAsync(Guid id)
        {
            var result = await _mediator.Send(new ActivatePlanCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Deactivate Plan")]
        [EndpointDescription("This endpoint deactivates the plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("deactivate/{id:guid}")]
        public async Task<IActionResult> DeactivatePlanAsync(Guid id)
        {
            var result = await _mediator.Send(new DeactivatePlanCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Make Plan Private")]
        [EndpointDescription("This endpoint set plan as private")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("make-private/{id:guid}")]
        public async Task<IActionResult> MakePrivateAsync(Guid id)
        {
            var result = await _mediator.Send(new MakePlanPrivateCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Make Plan Public")]
        [EndpointDescription("This endpoint set plan as public")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("make-public/{id:guid}")]
        public async Task<IActionResult> MakePublicAsync(Guid id)
        {
            var result = await _mediator.Send(new MakePlanPublicCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Update Plan Pricing")]
        [EndpointDescription("This endpoint update pricing of plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("update-pricing")]
        public async Task<IActionResult> UpdatePricingAsync(UpdatePlanPricingCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }
    }
}
