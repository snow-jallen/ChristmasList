using System.Collections.Generic;
using System.Threading.Tasks;
using ChristmasList.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChristmasList.Pages
{
    public class CatalogModel : PageModel
    {
        private readonly CatalogService catalogService;

        public CatalogModel(CatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public async Task OnGet()
        {
            Items = await catalogService.GetAllItemsAsync();

            DesiredItems = await catalogService.GetChildDesiredItemsAsync(User.Identity.Name);

            HotItems = await HotItemsModel.CreateHotItemsModelAsync(catalogService);
        }

        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Item> DesiredItems { get; set; }
        public HotItemsModel HotItems { get; set; }

        public async Task<IActionResult> OnPostIWantIt(int itemid)
        {            
            await catalogService.MarkWantedAsync(User.Identity.Name, itemid);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDontWantIt(int itemid)
        {
            DesiredItem desiredItem = await catalogService.GetDesiredItemAsync(User.Identity.Name, itemid);
            if (desiredItem == null)
                return Page();

            await catalogService.MarkNotWantedAsync(User.Identity.Name, itemid);

            return RedirectToPage();
        }
    }
}
