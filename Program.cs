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
        public static void Main(string[] args)
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);

            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();

            builder.Services.AddMvc();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(300);
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
            }

            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection().UseStaticFiles().UseRouting();

            /*
            EmailSender _emailSender = new EmailSender();
            Console.WriteLine("sending startup email for test");
            Dictionary<string, string> nameadressdict = new Dictionary<string, string>();
            nameadressdict.Add("Unifox", "awsomejojop@gmail.com");
            var message = new Message(nameadressdict, "Startup", "email is working", null);
            _emailSender.SendEmail(message);
            Console.WriteLine("email sent");
            */

            app.MapRazorPages();

            if (OperatingSystem.IsLinux())
            {
                app.Run("http://0.0.0.0:" + port);
            }
            else if (OperatingSystem.IsWindows())
            {
                app.Run("https://localhost:" + port);
            }
        }

        
    }
}
