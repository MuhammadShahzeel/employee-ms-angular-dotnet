using Microsoft.AspNetCore.Identity;

namespace EMSBackend.Models
{
    public class User :IdentityUser
    {
        public string? ProfileImage { get; set; }

    }
}
