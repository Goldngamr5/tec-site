using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using tec_site.Data;
using UniEncryption;

namespace tec_site.Pages.Accounts
{
    public class editModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly tec_siteData siteData = new();
        public string rResponse = String.Empty;
        public string uname = "";
        public string[]? userData = new string[0];

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
            uname = Request.Cookies["loggedIn"];
            userData = siteData.userInfo[uname];
            Console.WriteLine($"myaccount page for {uname}");
        }

        public async Task<ActionResult> OnPostEdit(string? newuname = null, string? newdisuname = null, string? newemail = null, string? psw = null, bool remember = true)
        {
            tec_siteData siteData = new();
            string encryptedpsw = psw.Encrypt();
            if (siteData.userInfo.ContainsKey(newuname))
            {
                rResponse = "Error: Username already exists!";
                return null;
            }
            else if (encryptedpsw != siteData.userInfo[uname][2])
            {
                rResponse = "Error: Incorrect password!";
                return null;
            }
            else if (newuname == null && newdisuname == null && newemail == null)
            {
                rResponse = "Error: Nothing changed!";
                return null;
            }
            else
            {
                Dictionary<string, string[]>? tempDict = siteData.userInfo;
                string[] TUI = { siteData.userInfo[uname][0], siteData.userInfo[uname][1], siteData.userInfo[uname][2], "User", "false" };
                if (newdisuname != null)
                {
                    TUI[0] = newdisuname;
                }
                if (newemail != null)
                {
                    TUI[0] = newemail;
                }

                if (newuname != null)
                {
                    tempDict.Add(newuname, TUI);
                    tempDict.Remove(uname);
                }
                else
                {
                    tempDict[uname] = TUI;
                }

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
                rResponse = "Editing... page will reload on next half hour...";
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

        public async Task<ActionResult> OnPostPswchange(string psw, string confpsw, string newpsw, string confnewpsw, bool remember)
        {
            tec_siteData siteData = new();
            string encryptedpsw = psw.Encrypt();
            string encryptedconfpsw = confpsw.Encrypt();
            string encryptednewpsw = newpsw.Encrypt();
            string encryptedconfnewpsw = confnewpsw.Encrypt();
            if (encryptedpsw != siteData.userInfo[uname][2])
            {
                rResponse = "Error: Incorrect password!";
                return null;
            }
            else if (encryptedconfpsw != siteData.userInfo[uname][2])
            {
                rResponse = "Error: Incorrect password!";
                return null;
            }
            else if (encryptedpsw != encryptedconfpsw)
            {
                rResponse = "Error: Current passwords don't match!";
                return null;
            }
            else if (encryptednewpsw != encryptedconfnewpsw)
            {
                rResponse = "Error: New passwords don't match!";
                return null;
            }
            else if (encryptedpsw != encryptednewpsw)
            {
                rResponse = "Error: New password same as current passowrd!";
                return null;
            }
            else if (psw == null && confpsw == null && newpsw == null && confnewpsw == null)
            {
                rResponse = "Error: Nothing changed!";
                return null;
            }
            else
            {
                Dictionary<string, string[]>? tempDict = siteData.userInfo;
                string[] TUI = { siteData.userInfo[uname][0], siteData.userInfo[uname][1], siteData.userInfo[uname][2], "User", "false" };

                TUI[2] = encryptednewpsw;
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
                rResponse = "Changing password... page will reload on next half hour...";
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
