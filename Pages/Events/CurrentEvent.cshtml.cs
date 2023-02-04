using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Globalization;

namespace tec_site.Pages
{
    public class CurrentEventModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public static bool pageloaded = false;

        public CurrentEventModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Current Event page accessed");
            pageloaded = true;
        }
    }
}