using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Invoices.AddInvoice;
using SaaSBillingSystem.Application.Features.Invoices.GetAllInvoices;
using SaaSBillingSystem.Application.Features.Invoices.GetInvoiceById;
using SaaSBillingSystem.Application.Features.Invoices.GetInvoicesByOrganization;
using SaaSBillingSystem.Application.Features.Invoices.GetInvoicesBySubscription;
using SaaSBillingSystem.Application.Features.Invoices.InvoiceExistsForPeriod;

namespace SaaSBillingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController: ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(AddInvoiceCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Value}, result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetInvoiceByIdCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllInvoicesCommand());
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpGet("existsforperiod")]
        public async Task<IActionResult> ExistsForPeriodAsync(InvoiceExistsForPeriodCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpGet("byorganization/{id:guid}")]
        public async Task<IActionResult> GetByOrganizationAsync(Guid id)
        {
            var result = await _mediator.Send(new GetInvoicesByOrganizationCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpGet("bysubscription/{id:guid}")]
        public async Task<IActionResult> GetBySubscriptionAsync(Guid id)
        {
            var result = await _mediator.Send(new GetInvoicesBySubscriptionCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }
    }
}
