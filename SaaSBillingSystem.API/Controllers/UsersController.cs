using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaSBillingSystem.Application.Features.Users.GetAllUsers;
using SaaSBillingSystem.Application.Features.Users.GetUserById;
using SaaSBillingSystem.Application.Features.Users.UserDtos;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.API.Controllers
{
    [Route("/api/{controller}")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            
            if(!result.IsSuccess)
            {
                return NotFound(ApiResponse<UserDto?>.FailureResponse(result.Error));
            }    
            
            return Ok(ApiResponse<UserDto?>.SuccessResponse(result.Value, "User Retrieved Successfully"));
        }
    }
}
