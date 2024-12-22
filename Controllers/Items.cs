using Microsoft.AspNetCore.Mvc;
using YourApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.IInterface;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class Items : Controller
    {
        //private readonly IDbContext _context;
        private readonly IItemservice _itemService;

        public Items(IItemservice itemService)
        {
            _itemService = itemService;
        }

        public async Task<IActionResult> CreateAsync()
        {
            var locations = await _itemService.GetAllLocationsAsync();
            ViewData["Locations"] = locations; // Pass to the view as ViewData
            return View();
          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Items item)
        {

            if (!ModelState.IsValid)
            {
                var location = await _itemService.GetLocationByIdAsync(item.LocationId);

                if (location != null)
                {
                    // Assign the Location to the Item's Location navigation property
                    item.Location = location;

                    // Add the Item via the service method
                    var result = await _itemService.AddItemAsync(item);
                    if (result)
                    {
                        return RedirectToAction(nameof(Index)); // Redirect to Index after adding
                    }
                    else
                    {
                        // Handle any errors here if needed
                        ModelState.AddModelError("", "Failed to add the item.");
                    }
                }
            }
                var locations = await _itemService.GetAllLocationsAsync();
                ViewData["Locations"] = locations; // Pass to the view as ViewData
                return View(item); // Show the form again if validation fails
            
        }

        // GET: Items
        public async Task<IActionResult> Index(string searchQuery)
        {
            var items = await _itemService.GetAllItemsAsync();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                items = items.Where(i =>
                    i.ItemTitle.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    i.ItemCode.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    (i.Location != null && i.Location.LocationTitle.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }


            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // Render only the table for AJAX requests
                return PartialView("_ItemsTable", items);
            }

            // Pass the search query and items for the full view
            ViewData["SearchQuery"] = searchQuery;
            return View(items);
        }


        public async Task<IActionResult> Edit(int Itemid)
        {
            // Fetch the item based on its ID
            var item = await _itemService.GetItemByIdAsync(Itemid);

            if (item == null)
            {
                return NotFound(); // Return 404 if the item doesn't exist
            }

            // Fetch locations for the dropdown in the edit form
            var locations = await _itemService.GetAllLocationsAsync();
            ViewData["Locations"] = locations;

            return View(item); // Pass the item to the view for editing
        }
      
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int itemId, Models.Items item)
        {
            

            if (!ModelState.IsValid)
            {
                try
                {
                    // Fetch the Location and assign it to the item
                    var location = await _itemService.GetLocationByIdAsync(item.LocationId);
                    if (location != null)
                    {
                        item.Location = location;
                    }

                    // Call the service to update the item
                    var result = await _itemService.UpdateItemAsync(item);
                    if (result)
                    {
                        return RedirectToAction(nameof(Index)); // Redirect to Index after updating
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to update the item.");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _itemService.ItemExists(item.ItemId))
                    {
                        return NotFound(); // Return 404 if the item doesn't exist
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Fetch locations again if the model is invalid
            var locations = await _itemService.GetAllLocationsAsync();
            ViewData["Locations"] = locations;

            return View(item); // Show the form again with error messages
        }
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id); // Implement GetItemByIdAsync if not already done
            if (item == null)
            {
                return NotFound(); // Handle not found
            }
            return View(item); // Show confirmation view
        }

        public async Task<IActionResult> ItemDetail(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id); // Implement GetItemByIdAsync if not already done
            if (item == null)
            {
                return NotFound(); // Handle not found
            }
            return View(item); // Show confirmation view
        }

        // POST: Perform Delete
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            if (!result)
            {
                return NotFound(); // Handle not found or error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

