using Microsoft.AspNetCore.Identity;

namespace myapi.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}