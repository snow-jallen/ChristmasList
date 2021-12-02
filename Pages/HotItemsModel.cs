using ChristmasList.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChristmasList.Pages
{
    public class HotItemsModel : PageModel
    {

        public static async Task<HotItemsModel> CreateHotItemsModel(ApplicationDbContext dbContext)
        {
            var model = new HotItemsModel();
            var desiredItems = await dbContext.DesiredItems
                .Include(di => di.Item)
                .ToListAsync();

            model.HotItems = (from di in desiredItems
                              group di by di.Item.Id into grp
                              orderby grp.Count() descending
                              select new HotItem
                              {
                                  Item = grp.First().Item,
                                  WantedBy = grp.Count()
                              }).Take(10);
            return model;
        }

        public DateTime RenderedOn { get; set; } = DateTime.Now;
        public IEnumerable<HotItem> HotItems { get; private set; }
    }

    public class HotItem
    {
        public Item Item { get; set; }
        public int WantedBy { get; set; }
    }
}
