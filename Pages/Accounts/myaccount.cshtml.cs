using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tec_site.Data;
using UniEncryption;

namespace tec_site.Pages.Accounts
{
    public class myaccountModel : PageModel
    {
        public string? uname = "";
        public string[]? userData = new string[0];

        public void OnGet()
        {
            tec_siteData siteData = new();
            uname = Request.Cookies["loggedIn"];
            userData = siteData.userInfo[uname];
            Console.WriteLine($"myaccount page for {uname}");
        }
    }
}
