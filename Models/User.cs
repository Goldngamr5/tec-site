using Microsoft.AspNetCore.Identity;

namespace tec_site.Models
{
    public class User : IdentityUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }

    }
}
