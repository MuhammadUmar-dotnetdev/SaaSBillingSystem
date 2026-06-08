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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllPlanFeaturesCommand());
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

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

        [HttpPatch("updatelimit")]
        public async Task<IActionResult> UpdateLimitAsync(UpdatePlanFeatureLimitCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

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
