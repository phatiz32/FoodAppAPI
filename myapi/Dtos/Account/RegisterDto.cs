using System.ComponentModel.DataAnnotations;

namespace myapi.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string? Fullname { get; set; }
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required]
        [Phone]
        public string? Phonenumber { get; set; }
        [Required]
        public string? Password { get; set; }

    }
}