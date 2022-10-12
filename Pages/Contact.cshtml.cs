using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tec_site.EmailService;

namespace tec_site.Pages
{
    public class ContactModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ContactModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Contact page acessed");
        }

        public ActionResult OnPostSend(string email, string usermessage)
        {
            Console.WriteLine("getting sender");
            EmailSender emailSender = new EmailSender();
            Console.WriteLine("setting email and message");

            Console.WriteLine("setting to dict");
            Dictionary<string, string> nameadressdict = new Dictionary<string, string>();
            nameadressdict.Add("TheEnergeticConvention", "theenergeticconvention@gmail.com");
            Console.WriteLine("Making Message");
            var message = new Message("WebsiteUser", email, nameadressdict, "Message from website", $"From: {email}\n{usermessage}", null);
            Console.WriteLine("Sending Message");
            emailSender.SendEmail(message);

            Console.WriteLine("redirecting");
            return RedirectToPage("Index");
        }
    }
}
