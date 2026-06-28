using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.FeaturesForPlan.AddFeature;
using SaaSBillingSystem.Application.Features.FeaturesForPlan.GetByKey;
using SaaSBillingSystem.Application.Features.FeaturesForPlan.RenameFeature;

namespace SaaSBillingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureController: ControllerBase
    {
        private readonly IMediator _mediator;
        public FeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [EndpointSummary("Add Feature")]
        [EndpointDescription("This endpoint creates a new Feature in a system uniquely identified by given key")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(AddFeatureCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Get Feature By Key")]
        [EndpointDescription("This endpoint returns Feature by given key")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("get-by-key/{key}")]
        public async Task<IActionResult> GetByKeyAsync(string key)
        {
            var result = await _mediator.Send(new GetByKeyCommand(key));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [EndpointSummary("Rename Feature")]
        [EndpointDescription("This endpoint renames existing Feature")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("rename")]
        public async Task<IActionResult> RenameAsync(RenameFeatureCommand command)
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
