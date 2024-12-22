using WebApplication3.Controllers;
using WebApplication3.Models;
using Items = WebApplication3.Models.Items;

namespace WebApplication3.IInterface
{
    public interface IItemservice
    {
        Task<IEnumerable<Models.Items>> GetAllItemsAsync();
        Task<Locations> GetLocationByIdAsync(int locationId);
        Task<List<Locations>> GetAllLocationsAsync();
        //Task<bool> AddItemAsync(Models.Items item);
        Task<bool> AddItemAsync(Models.Items item);
        Task<bool> AddLocationAsync(Models.Locations item);
        Task<bool> UpdateItemAsync(Models.Items item);
        Task<bool> ItemExists(int itemId);
        Task<bool> DeleteItemAsync(int itemId);
        Task<Items> GetItemByIdAsync(int itemId);
       
    }
}
