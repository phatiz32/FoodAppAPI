using myapi.Models;

namespace myapi.Interfaces
{
   public interface ITokenService
    {
        string CreatetToken(AppUser user);
    }

}