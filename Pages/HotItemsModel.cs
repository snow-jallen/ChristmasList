using ChristmasList.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChristmasList.Pages
{
    public class HotItemsModel
    {
        private readonly ApplicationDbContext dbContext;

        public HotItemsModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            init();
        }

        private async Task init()
        {
            var desiredItems = await dbContext.DesiredItems.Include(di => di.Item).ToListAsync();

            HotItems = from di in desiredItems
                       group di by di.Item.Id into grp
                       orderby grp.Count() descending
                       select new HotItem
                       {
                           Item = grp.First().Item,
                           WantedBy = grp.Count()
                       };
        }

        public DateTime RenderedOn { get; set; } = DateTime.Now;
        public IEnumerable<HotItem> HotItems { get; set; }
    }

    public class HotItem
    {
        public Item Item { get; set; }
        public int WantedBy { get; set; }
    }
}
