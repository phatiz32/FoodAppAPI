using myapi.Dtos.Account;

namespace myapi.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> SendResetPasswordEmailAsync(ForgotPassworDto forgotPassworDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    }
}