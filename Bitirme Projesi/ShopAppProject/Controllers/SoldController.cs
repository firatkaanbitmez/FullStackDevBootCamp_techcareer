// Controllers/SoldController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAppProject.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopAppProject.Controllers
{
    [Authorize(Roles = "Admin,Company")]
    public class SoldController : Controller
    {
        private readonly DataContext _context;

        public SoldController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sellerId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var solds = await _context.Solds
                .Include(s => s.Order)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(s => s.SellerId == sellerId)
                .OrderByDescending(s => s.SoldDate)
                .ToListAsync();

            return View(solds);
        }
        [HttpPost]
        public IActionResult UpdateShipmentInfo(int soldId, string shipmentTrackingInfo)
        {
            var sellerId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sold = _context.Solds
                        .Include(s => s.Order)
                        .FirstOrDefault(s => s.SoldId == soldId && s.SellerId == sellerId);

            if (sold != null)
            {
                // Update ShipmentTrackingInfo in Sold entity
                sold.ShipmentTrackingInfo = shipmentTrackingInfo;

                // Check if Order is not null and update ShipmentTrackingInfo in Order entity
                if (sold.Order != null)
                {
                    sold.Order.ShipmentTrackingInfo = shipmentTrackingInfo;
                }

                _context.SaveChanges();
                return RedirectToAction("Details", new { id = soldId });
            }

            return NotFound(); // Or handle the error appropriately
        }



        public async Task<IActionResult> Details(int id)
        {
            var sellerId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sold = await _context.Solds
                .Include(s => s.Order)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(s => s.SoldId == id && s.SellerId == sellerId);

            if (sold == null)
            {
                return NotFound();
            }

            return View(sold);
        }
    }
}
