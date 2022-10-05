using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace tec_site.Pages
{
    public class Event5Model : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Event5Model(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Udon Tycoon Fun Event page accessed");
        }
    }
}