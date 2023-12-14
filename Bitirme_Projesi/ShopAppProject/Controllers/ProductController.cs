using ShopAppProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopAppProject.Controllers
{
    [Authorize] // Requires authentication for all actions in this controller
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        // Herkesin görebileceği bir şekilde ürün listesini getir
        [AllowAnonymous] // Allow everyone to view products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            // Get the current user ID
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Set the user ID for the product
            model.UserId = userId;

            _context.Products.Add(model);
            await _context.SaveChangesAsync();

            // Set the entity state to Unchanged
            _context.Entry(model).State = EntityState.Unchanged;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id && p.UserId == userId);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product model)
        {
            if (id != model.ProductId)
            {
                return NotFound();
            }

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isAuthorized = _context.Products.Any(p => p.ProductId == id && p.UserId == userId);

            if (!isAuthorized)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // Log or handle validation errors
                return View(model);
            }

            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id && p.UserId == userId);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.ProductName = model.ProductName;
            existingProduct.ProductPrice = model.ProductPrice;
            existingProduct.ProductDesc = model.ProductDesc; // Ensure ProductDesc is updated

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id && p.UserId == userId);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id && p.UserId == userId);

            if (product == null)
            {
                return NotFound();
            }

            // Remove the product from the context and save changes
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            // Redirect to the Index action after the product has been deleted
            return RedirectToAction("Index");
        }
    }
}
