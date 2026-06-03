using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Plans.CreatePlan;
using SaaSBillingSystem.Application.Features.Plans.GetAllPlans;
using SaaSBillingSystem.Application.Features.Plans.GetPlanById;

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
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetPlanByIdCommand(id));
            
            if(!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPlansAsync()
        {
            var result = await _mediator.Send(new GetAllPlansCommand());
            if(!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
