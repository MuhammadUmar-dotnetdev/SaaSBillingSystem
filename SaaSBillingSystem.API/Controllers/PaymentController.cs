using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Invoices.GetAllInvoices;
using SaaSBillingSystem.Application.Features.Payments.AddPayment;
using SaaSBillingSystem.Application.Features.Payments.GetPaymentById;
using SaaSBillingSystem.Application.Features.Payments.GetPaymentsByInvoiceId;
using SaaSBillingSystem.Application.Features.Payments.HasSuccessfulPayment;
using SaaSBillingSystem.Application.Features.Payments.MarkPaymentAsFailed;
using SaaSBillingSystem.Application.Features.Payments.MarkPaymentAsPaid;
using SaaSBillingSystem.Application.Features.Payments.MarkPaymentAsRefund;
using SaaSBillingSystem.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SaaSBillingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController: ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(AddPaymentCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return Conflict(result.Error);
            }
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Value}, result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetPaymentByIdCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpGet("invoicepayments/{invoiceid:guid}")]
        public async Task<IActionResult> GetPaymentsByIdAsync(Guid invoiceid)
        {
            var result = await _mediator.Send(new GetPaymentsByInvoiceIdCommand(invoiceid));
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

        [HttpGet("has-successful-payment/{invoiceid:guid}")]
        public async Task<IActionResult> HasSuccessfulPayment(Guid invoiceid)
        {
            var result = await _mediator.Send(new HasSuccessfulPaymentCommand(invoiceid));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpPatch("mark-as-paid/{id:guid}")]
        public async Task<IActionResult> MarkAsPaid(Guid id)
        {
            var result = await _mediator.Send(new MarkPaymentAsPaidCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpPatch("mark-as-failed")]
        public async Task<IActionResult> MarkAsFailed(MarkPaymentAsFailedCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }

        [HttpPatch("mark-as-refund/{id:guid}")]
        public async Task<IActionResult> MarkAsRefund(Guid id)
        {
            var result = await _mediator.Send(new MarkPaymentAsRefundCommand(id));
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result);
        }
    }
}
