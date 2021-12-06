using System.Collections.Generic;
using System.Threading.Tasks;
using ChristmasList.Data;

namespace ChristmasList.Pages
{
    public interface ICatalogDataService
    {
        Task MarkWantedAsync(string child, int itemId);
        Task MarkNotWantedAsync(string child, int itemId);
        Task<IEnumerable<DesiredItem>> GetAllDesiredItemsAsync();
        Task<DesiredItem> GetDesiredItemAsync(string name, int itemid);
        Task<IEnumerable<Item>> GetAllItemsAsync();
    }
}
