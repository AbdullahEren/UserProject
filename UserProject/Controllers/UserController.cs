using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos.UserDto;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace UserProject.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
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
                var user = await _userService.GetOneUser(userName);
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
                var result = await _userService.UpdateUser(userName, userDto);

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
                var result = await _userService.DeleteOneUser(userName);

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

