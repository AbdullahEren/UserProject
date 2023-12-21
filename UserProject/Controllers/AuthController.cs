using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos.UserDto;
using Entities.Models;
using Services.Contracts;

namespace UserProject.Controllers
{
    
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegistrationDto userDto)
        {
            
            var result = await _authService.RegisterUser(userDto);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userDto)
        {
            if (!await _authService.ValidateUser(userDto))
            {
                return Unauthorized();
            }
            return Ok(new { Token = await _authService.CreateToken() });
        }
    }
}

