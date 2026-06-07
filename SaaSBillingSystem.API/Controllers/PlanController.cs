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

namespace SaaSBillingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController: ControllerBase
    {
        private readonly IMediator _mediator;
        public PlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPlansAsync()
        {
            var result = await _mediator.Send(new GetAllPlansCommand());
            if(!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

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

        [HttpPatch("changelimits")]
        public async Task<IActionResult> ChangeLimitsAsync(ChangeLimitsCommand command)
        {
            var result = await _mediator.Send(command);
            if(!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

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

        [HttpPatch("activate")]
        public async Task<IActionResult> ActivatePlan(ActivatePlanCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpPatch("deactivate")]
        public async Task<IActionResult> DeactivatePlan(DeactivatePlanCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpPatch("makeprivate")]
        public async Task<IActionResult> MakePrivate(MakePlanPrivateCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpPatch("makepublic")]
        public async Task<IActionResult> MakePublic(MakePlanPublicCommand command)
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
