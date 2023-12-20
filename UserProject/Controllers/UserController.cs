using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos.UserDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace UserProject.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _authService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUser(string userName)
        {
            try
            {
                var user = await _authService.GetOneUser(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{userName}")]
        public async Task<IActionResult> UpdateUser(string userName, [FromBody] UserForUpdateDto userDto)
        {
            try
            {
                var result = await _authService.UpdateUser(userName, userDto);

                if (result.Succeeded)
                {
                    return Ok("User updated successfully");
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            try
            {
                var result = await _authService.DeleteOneUser(userName);

                if (result.Succeeded)
                {
                    return Ok("User deleted successfully");
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

