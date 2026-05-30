using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SaaSBillingSystem.API.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [ApiController]
    [Route("api/{controller}")]
    public class AdminController: ControllerBase
    {
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            return Ok("Yes you admin");
        }
    }
}
