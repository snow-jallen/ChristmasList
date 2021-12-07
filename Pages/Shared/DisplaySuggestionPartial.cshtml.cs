using ChristmasList.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChristmasList.Pages.Shared
{
    public class DisplaySuggestionPartialModel : PageModel
    {
        public void OnGet()
        {
        }

        public Suggestion Suggestion { get; set; }
    }
}
