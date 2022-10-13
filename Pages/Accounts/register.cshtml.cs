using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using tec_site.Data;
using UniEncryption;

namespace tec_site.Pages.Accounts
{
    public class registerModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        public string rResponse = String.Empty;

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
            Console.WriteLine("register");
        }

        public async Task<ActionResult> OnPostRegister(string uname, string disuname, string email, string psw, string confpsw, bool remember = true)
        {
            tec_siteData siteData = new();
            if (siteData.userInfo.ContainsKey(uname))
            {
                rResponse = "Error: Username already exists!";
                return null;
            }
            else if (psw != confpsw)
            {
                rResponse = "Error: Passwords do not match!";
                return null;
            }
            else
            {
                string encryptedPass = psw.Encrypt();
                Dictionary<string, string[]>? tempDict = siteData.userInfo;
                string[] TUI = { disuname, email, encryptedPass, "User", "false" };
                tempDict.Add(uname, TUI);
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
                rResponse = "Registering... page will reload on next half hour...";
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
