using ChristmasList.Data;
using ChristmasList.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChristmasList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CatalogService catalogService;

        public IndexModel(ILogger<IndexModel> logger, CatalogService catalogService)
        {
            _logger = logger;
            this.catalogService = catalogService;
        }

        public async Task OnGet()
        {
            HotItems = await HotItemsModel.CreateHotItemsModel(catalogService);
            Suggestions = await dbContext.Suggestions
                .Include(s => s.ChildSuggestions)
                .OrderBy(s => s.AddedOn)
                .ToListAsync();
        }

        public HotItemsModel HotItems { get; set; }
        public List<Suggestion> Suggestions { get; set; }
        public AddSuggestionPartialModel AddSuggestionModel { get; set; } = new();

        public async Task<IActionResult> OnPostAsync(Suggestion NewSuggestion, int ParentSuggestionId)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            NewSuggestion.AddedOn = DateTime.Now;
            NewSuggestion.ParentSuggestionId = ParentSuggestionId;
            dbContext.Suggestions.Add(NewSuggestion);
            await dbContext.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
