using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.PlanFeatures.AddPlanFeature;
using SaaSBillingSystem.Application.Features.PlanFeatures.ChangePlanFeatureLimits;
using SaaSBillingSystem.Application.Features.PlanFeatures.DisablePlanFeature;
using SaaSBillingSystem.Application.Features.PlanFeatures.EnablePlanFeature;
using SaaSBillingSystem.Application.Features.PlanFeatures.GetFeaturesForPlan;
using SaaSBillingSystem.Application.Features.PlanFeatures.GetPlansForFeature;
using SaaSBillingSystem.Application.Features.PlanFeatures.RemovePlanFeature;

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
        public async Task<IActionResult> AddAsync(AddPlanFeatureCommand command)
        {
            var result = await _mediator.Send(command);
            if(!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            //return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Value }, result.Value);
            return Ok(result);
        }

        [EndpointSummary("Get Plans For A Feature")]
        [EndpointDescription("This endpoint returns Plans which owns Feature by its id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("plans-for-feature/{featureId:guid}")]
        public async Task<IActionResult> GetPlansForFeatureAsync(Guid featureId)
        {
            var result = await _mediator.Send(new GetPlansForFeatureCommand(featureId));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Get Features For A Plan")]
        [EndpointDescription("This endpoint returns Features which are owned by Plan by its id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("features-for-plan/{planId:guid}")]
        public async Task<IActionResult> GetFeaturesForPlanAsync(Guid planId)
        {
            var result = await _mediator.Send(new GetFeaturesForPlanCommand(planId));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Remove PlanFeatures")]
        [EndpointDescription("This endpoint returns Features which are owned by Plan by its id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemovePlanFeatureAsync(RemovePlanFeatureCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Enable PlanFeatures")]
        [EndpointDescription("This endpoint enables Features which are owned by Plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EnablePlanFeatureAsync(EnablePlanFeatureCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Disable PlanFeatures")]
        [EndpointDescription("This endpoint disables Features which are owned by Plan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DisablePlanFeatureAsync(DisablePlanFeatureCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Change PlanFeatures Limits")]
        [EndpointDescription("This endpoint changes limits of Features which are owned by Plans")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePlanFeatureLimitsAsync(ChangePlanFeatureLimitsCommand command)
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
