using ChristmasList.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        }

        public HotItemsModel HotItems { get; set; }
    }
}
