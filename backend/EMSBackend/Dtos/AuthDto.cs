using System.ComponentModel.DataAnnotations;

namespace EMSBackend.Dtos
{
    public class AuthDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
