using System.ComponentModel.DataAnnotations;

namespace myapi.Dtos.Account
{
    public class ForgotPassworDto
    {
         [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}