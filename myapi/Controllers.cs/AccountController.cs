
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapi.Dtos.Account;
using myapi.Interfaces;
using myapi.Models;

namespace myapi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null)
            {
                return Unauthorized("Invalid User");
            }
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Username not found or password incorrect");
            }
            else
            {
                return Ok(
                    new NewUserDto
                    {
                        Username = user.FullName,
                        Email = user.Email,
                        Token=_tokenService.CreatetToken(user)
                    }
                );
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                    var appUser = new AppUser
                    {
                        UserName=registerDto.Email,
                        FullName = registerDto.Fullname,
                        Email = registerDto.Email,
                        PhoneNumber = registerDto.Phonenumber,
                    };
                    var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                    if (createdUser.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                        if (roleResult.Succeeded)
                        {
                            return Ok(
                                new NewUserDto
                                {
                                    Username = appUser.FullName,
                                    Email = appUser.Email,
                                    Token=_tokenService.CreatetToken(appUser)
                                }
                            );

                        }
                        else
                        {
                            return StatusCode(500, roleResult.Errors);
                        }
                    }
                    else
                    {
                        return StatusCode(500, createdUser.Errors);
                    }

                

            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

    }
    
}