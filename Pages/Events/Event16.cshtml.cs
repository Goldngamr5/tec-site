using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace tec_site.Pages
{
    public class Event16Model : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Event16Model(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("DJ stream Event page accessed");
        }
    }
}