using Microsoft.AspNetCore.Identity;

namespace tec_site.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string DiscordUser { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }

    }
}
