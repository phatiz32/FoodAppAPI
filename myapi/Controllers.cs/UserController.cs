using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myapi.Dtos.User;
using myapi.Interfaces;
using myapi.Models;

namespace myapi.Controllers.cs
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserContronller : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        public UserContronller(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetMyInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var userDto = await _userRepository.GetByIdAsync(user.Id);
            return Ok(userDto);
        }
        [HttpPut("me")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound("User not found");

            var updated = await _userRepository.UpdateAsync(user.Id, dto);
            if (!updated) return BadRequest("Update failed");

            return NoContent();
        }
    }
}