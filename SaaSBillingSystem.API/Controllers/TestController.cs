using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SaaSBillingSystem.API.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class TestController: ControllerBase
    {
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            return Ok("Yes you");
        }
    }
}
