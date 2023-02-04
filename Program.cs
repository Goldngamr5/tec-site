using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using tec_site.Data;
using tec_site.EmailService;
using tec_site.Models;
using AutoMapper;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;

namespace tec_site
{
    public class Program
    {

        public static DateTime mainNow = DateTime.Now;
        public static DateTime startuptime = mainNow;

        public static DateTime GetEventTime(string eventdate)
        {
            return DateTime.ParseExact(eventdate, "dd/MM/yyyy hh:mm:ss tt", null);
        }

        public static string reformatDate(string date)
        {
            char[] splitdel = { ' ', '/' };
            string[] splitdate = date.Split(splitdel);
            string finaldate = splitdate[2] + "/" + splitdate[1] + "/" + splitdate[0] + " " + splitdate[3] + " " + splitdate[4];
            Console.WriteLine(finaldate);
            return finaldate;
        }

        public static string Event1Time = "31/03/2023 04:00:00 PM";
        public static string Event2Time = "31/03/2023 05:00:00 PM";
        public static string Event3Time = "31/03/2023 06:00:00 PM";
        public static string Event4Time = "31/03/2023 07:00:00 PM";
        public static string Day1EndTime = "31/03/2023 08:00:00 PM";
        public static string Event5Time = "01/04/2023 08:00:00 AM";
        public static string Event6Time = "01/04/2023 09:00:00 AM";
        public static string Event7Time = "01/04/2023 11:00:00 AM";
        public static string Event8Time = "01/04/2023 12:00:00 PM";
        public static string Event9Time = "01/04/2023 12:30:00 PM";
        public static string Event10Time = "01/04/2023 01:00:00 PM";
        public static string Event11Time = "01/04/2023 02:00:00 PM";
        public static string Event12Time = "01/04/2023 03:00:00 PM";
        public static string Event13Time = "01/04/2023 04:00:00 PM";
        public static string Event14Time = "01/04/2023 05:00:00 PM";
        public static string Event15Time = "01/04/2023 06:00:00 PM";
        public static string Event16Time = "01/04/2023 07:00:00 PM";
        public static string Event17Time = "01/04/2023 08:00:00 PM";
        public static string Day2EndTime = "01/04/2023 09:00:00 PM";
        public static string Event18Time = "02/04/2023 08:00:00 AM";
        public static string Event19Time = "02/04/2023 09:00:00 AM";
        public static string Event20Time = "02/04/2023 10:00:00 AM";
        public static string Event21Time = "02/04/2023 11:00:00 AM";
        public static string Event22Time = "02/04/2023 12:00:00 PM";
        public static string Event23Time = "02/04/2023 01:00:00 PM";
        public static string Event24Time = "02/04/2023 03:00:00 PM";
        public static string Event25Time = "02/04/2023 04:00:00 PM";
        public static string Event26Time = "02/04/2023 05:00:00 PM";
        public static string ConEndTime = "02/04/2023 06:00:00 PM";

        public static async Task<string> UpdateDNS(string IP, string DNSid, string name)
        {
            string reqContent = "{\n    \"content\": \"" + IP + "\",\n    \"name\": \"" + name + "\",\n    \"ttl\": 1,\n    \"type\": \"A\"\n}";
            StringContent reqStr = new StringContent(reqContent, System.Text.Encoding.ASCII, "application/json");
            //JsonContent reqjson = JsonContent.Create(reqContent);
            Console.WriteLine(await reqStr.ReadAsStringAsync());
            reqStr.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            Console.WriteLine(reqStr.Headers);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("CFBToken"));
            client.DefaultRequestHeaders.Add("X-Auth-Email", "awsomejojop@gmail.com");
            Console.WriteLine(client.DefaultRequestHeaders.ToString());
            string reqUrl = "https://api.cloudflare.com/client/v4/zones/97e43709fe00a86833d432e2bd9c06e9/dns_records/" + DNSid;
            Console.WriteLine(reqUrl);
            var resp = await client.PutAsync(reqUrl, reqStr);
            Console.WriteLine(await resp.Content.ReadAsStringAsync());
            return resp.ToString();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args, string myIP, string root)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(root)
                .UseUrls("https://" + myIP, "http://" + myIP)
                .UseIISIntegration()
                .UseStartup<Startup>();
            return host;
        }

        public static async Task Main(string[] args)
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);

            var port = Environment.GetEnvironmentVariable("PORT") ?? "443";


            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            Console.WriteLine(hostName);
            // Get the IP
            IPAddress[] myIPs = Dns.GetHostEntry(hostName).AddressList;
            string myIP = myIPs[myIPs.Length - 1].ToString();
            Console.WriteLine("My IP Address is :" + myIP);


            var builder = CreateHostBuilder(args, myIP, root);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
            }

            /*
            EmailSender _emailSender = new EmailSender();
            Console.WriteLine("sending startup email for test");
            Dictionary<string, string> nameadressdict = new Dictionary<string, string>();
            nameadressdict.Add("Unifox", "awsomejojop@gmail.com");
            var message = new Message("The Energetic Convention", "theenergeticconvention@gmail.com", nameadressdict, "Startup", "<html><style>a {color: rgb(255, 210, 8)} a:hover {color: rgb(220, 180, 0)} body {margin-bottom: 30px; background-repeat: no-repeat; background-size: cover; background-position-y: 25 %;background-color: rgba(33, 37, 41, 1); opacity: 1; width: 99 %; height: 90 %;} p {color: rgb(255, 255, 255)}</style><body>test text <a href='https://discord.gg/rGBn2uW'>test link</a></body><html>", null);
            _emailSender.SendEmail(message);
            Console.WriteLine("email sent");
            */
            
            string strRIP = await new HttpClient().GetStringAsync("https://ipinfo.io/ip");
            string[] toWrite = { strRIP };
            Console.WriteLine("Router IP:" + strRIP);

            string routerIP = Environment.GetEnvironmentVariable("RIP") ?? "0.0.0.0";

            if (routerIP == "0.0.0.0")
            {
                Console.Error.WriteLine("ERROR! NO ROUTER IP FOUND");
            }
            else if (routerIP != strRIP)
            {
                for (int i = 0; i < 2; i++)
                {
                    string dnsID, webname;
                    if (i == 0)
                    {
                        dnsID = "ac5e3653e2c8a4e76ec141b416e6beb6";
                        webname = "thenergeticon.com";
                    }
                    else
                    {
                        dnsID = "55b26fe22e78fb9ef7dc394e6f6d88de";
                        webname = "www.thenergeticon.com";
                    }

                    string CFresp = await UpdateDNS(strRIP, dnsID, webname);
                    Console.WriteLine(CFresp);
                }
                DotEnv.Write(dotenv, "RIP = " + strRIP);
                Environment.SetEnvironmentVariable("RIP", strRIP);
            }

            app.RunAsync();

            while (true)
            {
                mainNow = DateTime.Now;
                Console.WriteLine(mainNow.ToString());
                if (mainNow.ToLongTimeString() == "12:00:00 AM")
                {
                    Environment.Exit(0);
                }
                Thread.Sleep(500);
            }
        }

        
    }
}
