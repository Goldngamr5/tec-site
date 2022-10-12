using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace tec_site.Pages
{
    public class tecapiModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public tecapiModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("API page acessed");
        }
    }
}