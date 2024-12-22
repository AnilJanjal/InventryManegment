using Microsoft.EntityFrameworkCore;
using WebApplication3.Controllers;
using WebApplication3.IInterface;
using WebApplication3.Models;
using YourApp.Models;
using Items = WebApplication3.Models.Items;
namespace WebApplication3.DataAcessl
{
    public class ItemsSevice : IItemservice
    {

        private readonly IDbContext _context;

        public ItemsSevice(IDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Items>> GetAllItemsAsync()
        {
            return await _context.Items.Include(i => i.Location).ToListAsync(); // Fetch all items asynchronously
        }
        public async Task<bool> AddItemAsync(Models.Items item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return true; 
        }
        public async Task<bool> AddLocationAsync(Locations locations)
        {
            _context.Locations.Add(locations);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateItemAsync(Models.Items item)
        {
            try
            {
                var existingItem = await _context.Items.FindAsync(item.ItemId);
                if (existingItem == null)
                {
                    return false; // Item not found
                }

                // Update only the fields that are modified (optional but better practice)
                existingItem.ItemCode = item.ItemCode;
                existingItem.ItemTitle = item.ItemTitle;
                existingItem.Description = item.Description;
                existingItem.SalePrice = item.SalePrice;
                existingItem.PurchasePrice = item.PurchasePrice;
                existingItem.LocationId = item.LocationId;
                existingItem.IsActive = item.IsActive;

                // Save changes
                await _context.SaveChangesAsync();
                return true; // Successfully updated
            }
            catch (Exception)
            {
                // Handle exception if needed
                return false; // Update failed
            }
        }
        public async Task<bool> DeleteItemAsync(int itemId)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item == null)
            {
                return false; // Item not found
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ItemExists(int itemId)
        {
            return true;
        }
        public async Task<Items> GetItemByIdAsync(int itemId)
        {
            return await _context.Items.FindAsync(itemId);
        }
        public async Task<Locations> GetLocationByIdAsync(int locationId)
        {
            return await _context.Locations.FindAsync(locationId);
        }
        public async Task<List<Locations>> GetAllLocationsAsync()
        {
            return await _context.Locations.ToListAsync();
        }
        public Task<bool> AddItemAsync(Controllers.Items item)
        {
            throw new NotImplementedException();
        }
    }
}
