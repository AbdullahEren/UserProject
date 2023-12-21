using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos.UserDto;
using Entities.Models;
using Services.Contracts;

namespace UserProject.Controllers
{
    [Route("api/auth")]
    [ApiController]
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

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
            
        }

        
    }
}

