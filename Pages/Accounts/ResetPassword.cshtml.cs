using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tec_site.Data;
using UniEncryption;
using Newtonsoft.Json;

namespace tec_site.Pages.Accounts
{
    public class ResetPasswordModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        public string rResponse = String.Empty;
        public bool isCookie = false;

        private void WaitUntil(DateTime dateTime)
        {
            while (true)
            {
                if (DateTime.Now >= dateTime)
                {
                    return;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        public void OnGet()
        {
            Console.WriteLine("ForgotPsw");
            isCookie = (Request.Cookies["resetPsw"] != null);
        }

        public async Task<ActionResult> OnPostSendreset(string psw, string confpsw, bool remember)
        {
            tec_siteData siteData = new();
            string encryptedpsw = psw.Encrypt();
            string encryptedconfpsw = confpsw.Encrypt();
            if (encryptedpsw != encryptedconfpsw)
            {
                rResponse = "Error: Passwords don't match!";
                return null;
            }
            else if (psw == null && confpsw == null)
            {
                rResponse = "Error: Nothing changed!";
                return null;
            }
            else
            {
                string uname = Request.Cookies["resetPsw"];
                Dictionary<string, string[]>? tempDict = siteData.userInfo;
                string[] TUI = { siteData.userInfo[uname][0], siteData.userInfo[uname][1], siteData.userInfo[uname][2], "User", "false" };

                TUI[2] = encryptedpsw;
                tempDict[uname] = TUI;

                siteData.userInfo = tempDict;

                var values = new Dictionary<string, Dictionary<string, string[]>>
                {
                    { "USERINFO", tempDict }
                };

                var json = JsonConvert.SerializeObject(values);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Patch,
                    RequestUri = new Uri("https://api.heroku.com/apps/tec-site/config-vars"),
                    Headers = {
                            { "Accept", "application/vnd.heroku+json; version=3" },
                            { "Authorization", $"Bearer {Environment.GetEnvironmentVariable("HEROKU_API_KEY")}" }
                        },
                    Content = content
                };
                var nextHlfHr = DateTime.Now;
                if (nextHlfHr.Minute == 0)
                {
                    nextHlfHr.AddMinutes(30);
                }
                else
                {
                    nextHlfHr.AddMinutes(30 - nextHlfHr.Minute);
                }
                rResponse = "Resetting password... page will reload on next half hour...";
                await Task.Run(() => WaitUntil(nextHlfHr)); 
                var response = await client.SendAsync(httpRequestMessage);
                var responseContent = response.Content.ReadAsStringAsync();
                Console.WriteLine(response);
                Console.WriteLine(responseContent.Result);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    rResponse = "Success!";
                    CookieOptions cookieOptions = new();
                    if (remember)
                    {
                        cookieOptions.Expires = DateTime.MaxValue;
                    }
                    Response.Cookies.Delete("resetPsw");
                    Response.Cookies.Delete("loggedIn");
                    Response.Cookies.Append("loggedIn", uname, cookieOptions);
                    return RedirectToPage("../Index");
                }
                else
                {
                    rResponse = $"Error: got {response.StatusCode} response code... Please contact us about this so we can check what happened!";
                    return null;
                }
            }
        }
    }
}
