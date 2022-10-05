using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace tec_site.Pages
{
    public class AlwaysOpenModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public AlwaysOpenModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("AlwaysOpen page accessed");
        }
    }
}