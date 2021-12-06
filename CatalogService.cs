using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristmasList.Data;

namespace ChristmasList.Pages
{
    public class CatalogService
    {
        private readonly ICatalogDataService catalogDataService;

        public CatalogService(ICatalogDataService catalogDataService)
        {
            this.catalogDataService = catalogDataService;
        }

        public async Task<IEnumerable<Item>> GetChildDesiredItemsAsync(string child)
        {
            var desiredItems = await catalogDataService.GetAllDesiredItemsAsync();
            return desiredItems
                .Where(di => di.ChildEmail == child)
                .Select(di => di.Item);
        }

        public async Task<IEnumerable<DesiredItem>> GetAllDesiredItemsAsync() => 
            await catalogDataService.GetAllDesiredItemsAsync();

        internal async Task MarkWantedAsync(string name, int itemid)
        {
            await catalogDataService.MarkWantedAsync(name, itemid);
        }

        internal async Task<DesiredItem> GetDesiredItemAsync(string name, int itemid)
        {
            return await catalogDataService.GetDesiredItemAsync(name, itemid);
        }

        internal async Task MarkNotWantedAsync(string name, int itemid)
        {
            await catalogDataService.MarkNotWantedAsync(name, itemid);
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync() =>
            await catalogDataService.GetAllItemsAsync();
    }
}
