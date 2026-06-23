using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.PlanFeatures.AddPlanFeature;
using SaaSBillingSystem.Application.Features.PlanFeatures.DisablePlanFeature;
using SaaSBillingSystem.Application.Features.PlanFeatures.EnablePlanFeature;
using SaaSBillingSystem.Application.Features.PlanFeatures.GetAllPlanFeatures;
using SaaSBillingSystem.Application.Features.PlanFeatures.GetPlanFeatureById;
using SaaSBillingSystem.Application.Features.PlanFeatures.RenamePlanFeature;
using SaaSBillingSystem.Application.Features.PlanFeatures.UpdatePlanFeatureLimit;

namespace SaaSBillingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanFeatureController: ControllerBase
    {
        private readonly IMediator _mediator;
        public PlanFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EndpointSummary("Create PlanFeature")]
        [EndpointDescription("This endpoint creates new planfeature in system")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] AddPlanFeatureCommand command)
        {
            var result = await _mediator.Send(command);
            if(!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Value!.Id }, result.Value);
        }

        [EndpointSummary("Get PlanFeature By Id")]
        [EndpointDescription("This endpoint returns PlanFeature by it id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetPlanFeatureByIdCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Get All PlanFeatures")]
        [EndpointDescription("This endpoint returns all PlanFeatures in a system")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllPlanFeaturesCommand());
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Enable PlanFeatures")]
        [EndpointDescription("This endpoint enable PlanFeature by id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("enable")]
        public async Task<IActionResult> EnableAsync(Guid id)
        {
            var result = await _mediator.Send(new EnablePlanFeatureCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Disable PlanFeatures")]
        [EndpointDescription("This endpoint disable PlanFeature by id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("disable")]
        public async Task<IActionResult> DisableAsync(Guid id)
        {
            var result = await _mediator.Send(new DisablePlanFeatureCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Update PlanFeature Limits")]
        [EndpointDescription("This endpoint updates the limits of PlanFeature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("update-limit")]
        public async Task<IActionResult> UpdateLimitAsync(UpdatePlanFeatureLimitCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Rename PlanFeature")]
        [EndpointDescription("This endpoint rename the PlanFeature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("rename")]
        public async Task<IActionResult> RenameAsync(RenamePlanFeatureCommand command)
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
