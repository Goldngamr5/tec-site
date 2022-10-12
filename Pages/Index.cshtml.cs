using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using tec_site.Data;
using UniEncryption;
using Newtonsoft.Json;
using System.Net.Http;
using System;

namespace tec_site.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private static readonly HttpClient client = new HttpClient();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Home page acessed");
        }
    }
}