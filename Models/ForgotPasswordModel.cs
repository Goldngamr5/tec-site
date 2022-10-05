using System.ComponentModel.DataAnnotations;

namespace tec_site.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
