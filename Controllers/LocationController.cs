using Microsoft.AspNetCore.Mvc;
using WebApplication3.IInterface;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class LocationController : Controller
    {
        private readonly IItemservice _itemService;
        public LocationController(IItemservice iIservices)
        {
            _itemService = iIservices;
        }
         public async Task<IActionResult> Index()
        {
            //var items = await _context.Items.ToListAsync();
            var items = await _itemService.GetAllLocationsAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Locations locations)
        {
            var result = await _itemService.AddLocationAsync(locations);
            if (result)
            {
                return RedirectToAction(nameof(Index)); // Redirect to Index after adding
            }
            else
            {
                // Handle any errors here if needed
                return NotFound();
            }
        }

      }
}
