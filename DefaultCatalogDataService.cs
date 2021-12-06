using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristmasList.Data;
using Microsoft.EntityFrameworkCore;

namespace ChristmasList.Pages
{
    public class DefaultCatalogDataService : ICatalogDataService
    {
        private readonly ApplicationDbContext dbContext;

        public DefaultCatalogDataService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<DesiredItem>> GetAllDesiredItemsAsync()
        {
            return await dbContext.DesiredItems
                .Include(di=>di.Item)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await dbContext.Items.ToListAsync();
        }

        public async Task<DesiredItem> GetDesiredItemAsync(string name, int itemid)
        {
            return await dbContext.DesiredItems.FirstOrDefaultAsync(di => di.ChildEmail == name && di.ItemId == itemid);
        }

        public async Task MarkNotWantedAsync(string child, int itemId)
        {
            var desiredItem = await GetDesiredItemAsync(child, itemId);
            if (desiredItem == null)
                throw new Exception("Unable to locate desired item");

            dbContext.DesiredItems.Remove(desiredItem);
            await dbContext.SaveChangesAsync();
        }

        public async Task MarkWantedAsync(string child, int itemId)
        {
            var desiredItem = new DesiredItem
            {
                ChildEmail = child,
                ItemId = itemId
            };
            await dbContext.DesiredItems.AddAsync(desiredItem);
            await dbContext.SaveChangesAsync();
        }
    }
}
