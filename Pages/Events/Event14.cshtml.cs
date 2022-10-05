using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace tec_site.Pages
{
    public class Event14Model : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Event14Model(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Fursuit Meet & Greet Event page accessed");
        }
    }
}