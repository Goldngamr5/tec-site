using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;

namespace tec_site.Data
{
    public class tec_siteData
    {
        public Dictionary<string, string[]>? userInfo;

        public tec_siteData()
        {
            string? userInfoStr = Environment.GetEnvironmentVariable("USERINFO");
            
            if (userInfoStr != null)
            {
                userInfoStr = userInfoStr.Replace("\"=>[", "\":[");
                userInfo = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(userInfoStr);
            }
        }
    }
}
