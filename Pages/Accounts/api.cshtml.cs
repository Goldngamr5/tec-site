using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using tec_site.Data;

namespace tec_site.Pages.Accounts
{
    public class apiModel : PageModel
    {
        public string userinfo = String.Empty;

        private readonly ILogger<PrivacyModel> _logger;

        public apiModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
            Console.WriteLine("accounts api Page made");
        }

        public ActionResult OnGet()
        {
            Console.WriteLine("api accounts get");
            tec_siteData siteData = new();
            string[] users = siteData.userInfo.Keys.ToArray();
            Dictionary<string, string> data = new Dictionary<string, string>();
            for (int i = 0; i < users.Length; i++)
            {
                data.Add(users[i], siteData.userInfo[users[i]][0]);
            }
            userinfo = JsonConvert.SerializeObject(data);
            Console.WriteLine("api accounts get done");
            return new OkObjectResult(userinfo);
        }
    }
}
