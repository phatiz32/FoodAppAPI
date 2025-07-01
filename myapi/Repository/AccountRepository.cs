using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.CodeAnalysis.Emit;
using myapi.Data;
using myapi.Dtos.Account;
using myapi.Interfaces;
using myapi.Models;

namespace myapi.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailServices _emailService;
        public AccountRepository(UserManager<AppUser> userManager, IEmailServices emailServices)
        {
            _userManager = userManager;
            _emailService = emailServices;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return false;

            }
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            return result.Succeeded;
        }

        public async Task<bool> SendResetPasswordEmailAsync(ForgotPassworDto forgotPassworDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassworDto.Email);
            if (user == null)
            {
                return false;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://localhost:5001/reset-password?email={Uri.EscapeDataString(user.Email)}&token ={Uri.EscapeDataString(token)})";
            var body = $@"
                <h2>Yêu cầu đặt lại mật khẩu</h2>
                <p>Nhấn vào liên kết bên dưới để đặt lại mật khẩu:</p>
                <a href='{resetLink}'>Đặt lại mật khẩu</a>
            ";
            await _emailService.SendMailAsync(user.Email, "Đặt lại mật khẩu TPFood", body);
            return true;
        }
    }
}