using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Auth.LoginUser;
using SaaSBillingSystem.Application.Features.Auth.RegisterUser;
using SaaSBillingSystem.Application.Features.Login;

namespace SaaSBillingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand request)
    {
        var result = await _mediator.Send(request);

        if(!result.IsSuccess)
        {
            return Unauthorized(new
            {
                Message = "Invalid email or password"
            });
        }

        return Ok(new
        {
            Response = result.Value
        });
    }
}