using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace tec_site.Pages.Events
{
    public class apiModel : PageModel
    {
        public string events = String.Empty;
        private readonly string[] eventsList = { "opening", "prophunt", "furrygames", "mcminigames", "udontycoon", "udontag", "minesweeper", "amongus", "bsbattles", "cah", "fandf", "puttputt", "closing", "furmeet", "jackbox", "djstream" };

        public ActionResult OnGet()
        {
            Console.WriteLine("api events get");
            events = JsonConvert.SerializeObject(eventsList);
            Console.WriteLine("api events get done");
            return new OkObjectResult(events); 
        }
    }
}
