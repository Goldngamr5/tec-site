using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tec_site.EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public Message(Dictionary<string, string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = new List<MailboxAddress>();

            foreach (string key in to.Keys)
            {
                To.Add(new MailboxAddress(key, to[key]));
            }
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
