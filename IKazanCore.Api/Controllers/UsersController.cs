using IKazanCore.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace IKazanCore.Api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ApplicationControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var abc = await _userService.LoginAsync();        

            return StatusCode(StatusCodes.Status200OK, abc);
        }

        [HttpGet("secure")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Secure()
        {
            var userId = GetUserId();
            return StatusCode(StatusCodes.Status200OK, new { userId });
        }
    }
}