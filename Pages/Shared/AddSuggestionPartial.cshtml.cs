using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristmasList.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChristmasList.Pages.Shared
{
    public class AddSuggestionPartialModel : PageModel
    {
        public Suggestion NewSuggestion { get; set; } = new();
        public int ParentSuggestionId { get; set; }

        public AddSuggestionPartialModel()
        {

        }

        public AddSuggestionPartialModel(int parentSuggestionId)
        {
            ParentSuggestionId = parentSuggestionId;
        }
    }
}
