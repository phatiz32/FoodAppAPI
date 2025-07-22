using myapi.Models;

namespace myapi.Interfaces
{
   public interface ITokenService
    {
        Task<string> CreatetToken(AppUser user);
    }

}