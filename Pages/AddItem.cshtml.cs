using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristmasList.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChristmasList.Pages
{
    [Authorize]
    public class AddItemModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public AddItemModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public Item NewItem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            dbContext.Items.Add(NewItem);
            await dbContext.SaveChangesAsync();

            return RedirectToPage("Catalog");
        }
    }
}
