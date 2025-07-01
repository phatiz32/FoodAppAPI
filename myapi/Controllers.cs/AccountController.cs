
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myapi.Dtos.Account;
using myapi.Interfaces;
using myapi.Models;
using myapi.Service;

namespace myapi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IEmailServices _emailService;
        private readonly IAccountRepository _accountRepo;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager, IEmailServices emailServices, IAccountRepository accountRepo)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _emailService = emailServices;
            _accountRepo = accountRepo;
            
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
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPassworDto dto)
        {
            var result = await _accountRepo.SendResetPasswordEmailAsync(dto);
            if (!result)
            {
                return NotFound("Email không tồn tại.");
            }
            return Ok("Đã gửi email đặt lại mật khẩu!");
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _accountRepo.ResetPasswordAsync(dto);
            if (!result)
            {
                return BadRequest("Không thể đặt lại mật khẩu. Vui lòng kiểm tra lại token hoặc email.");
            }
            return Ok("Đặt lại mật khẩu thành công!");
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
                    var existingEmail = await _userManager.FindByEmailAsync(registerDto.Email);
                    if (existingEmail != null)
                    {
                         return BadRequest("Email đã tồn tại ");
                    }
                    var phoneExists = await _userManager.Users
                        .AnyAsync(u => u.PhoneNumber == registerDto.Phonenumber);
                    if (phoneExists)
                    {
                        return BadRequest("Số điện thoại đã tồn tại.");
                    }
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