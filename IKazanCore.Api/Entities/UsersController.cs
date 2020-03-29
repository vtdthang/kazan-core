using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IKazanCore.Api.Entities
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            var abc = new
            {
                id = 1,
                name = "Test 1"
            };

            return StatusCode(StatusCodes.Status200OK, abc);
        }
    }
}