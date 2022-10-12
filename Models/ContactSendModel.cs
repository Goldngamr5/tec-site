using System.ComponentModel.DataAnnotations;

namespace tec_site.Models
{
    public class ContactSendModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [DataType(DataType.Text)]
        public string Message { get; set; }
    }
}
