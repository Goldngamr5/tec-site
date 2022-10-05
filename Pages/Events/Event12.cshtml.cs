using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace tec_site.Pages
{
    public class Event12Model : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Event12Model(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Putt Putt Quest Event page accessed");
        }
    }
}