using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristmasList.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChristmasList.Pages
{
    public class CatalogModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public CatalogModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task OnGet()
        {
            await loadPageData();
        }

        private async Task loadPageData()
        {
            Items = await dbContext.Items.ToListAsync();
            DesiredItems = await dbContext.DesiredItems
                .Include(di => di.Item)
                .Where(di => di.ChildEmail == User.Identity.Name)
                .Select(di=>di.Item).ToListAsync();
        }

        public List<Item> Items { get; set; }
        public List<Item> DesiredItems { get; set; }

        public async Task<IActionResult> OnGetIWantIt(int itemid)
        {
            var desiredItem = new DesiredItem
            {
                ChildEmail = User.Identity.Name,
                ItemId = itemid
            };
            await dbContext.DesiredItems.AddAsync(desiredItem);
            await dbContext.SaveChangesAsync();

            return Page();
        }

        public async Task<IActionResult> OnGetDontWantIt(int itemid)
        {
            var desiredItem = await dbContext.DesiredItems.FirstOrDefaultAsync(di => di.ChildEmail == User.Identity.Name && di.ItemId == itemid);
            if (desiredItem == null)
                return Page();

            dbContext.DesiredItems.Remove(desiredItem);
            await dbContext.SaveChangesAsync();

            return Page();
        }
    }
}
