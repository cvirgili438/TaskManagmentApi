using Microsoft.AspNetCore.Mvc;
using TaskApplicationApi.Application.Models.Security;

namespace TaskApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
            
        }
        [HttpPost]
        public async Task<ActionResult> LogIn([FromBody] LogInRequest request) 
        {
            return BadRequest();
        }
    }
}
