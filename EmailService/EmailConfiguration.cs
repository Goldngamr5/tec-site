using System;
using System.Collections.Generic;
using System.Text;

namespace tec_site.EmailService
{
    public class EmailConfiguration
    {
        public string From { get; }
        public string SmtpServer { get; } 
        public int Port { get; } 
        public string UserName { get; } 
        public string Password { get; }

        public EmailConfiguration()
        {
            From = "theenergeticconvention@gmail.com";
            SmtpServer = "smtp.gmail.com";
            Port = 465;
            UserName = "theenergeticconvention@gmail.com";
            Console.WriteLine(Environment.GetEnvironmentVariable("SMTPPASS"));
            Password = Environment.GetEnvironmentVariable("SMTPPASS");
            Console.WriteLine(Password);
        }
    }
}
