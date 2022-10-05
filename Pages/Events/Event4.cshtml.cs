using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace tec_site.Pages
{
    public class Event4Model : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Event4Model(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Minecraft mini games Event page accessed");
        }
    }
}