using ShopAppProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopAppProject.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }

        private async Task<int> AddToCartAsync(int productId)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                // Add logic to get the seller ID based on the product
                var sellerId = await _context.Products
                    .Where(p => p.ProductId == productId)
                    .Select(p => p.UserId)
                    .FirstOrDefaultAsync();

                // Add logic to add to cart with the seller ID
                // ...

                await _context.SaveChangesAsync();

                return await _context.CartItems.CountAsync(c => c.UserId == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding to cart: {ex.Message}");
                return -1;
            }
        }

        [HttpGet]
        public IActionResult GetCartItemCount()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItemCount = _context.CartItems.Count(c => c.UserId == userId);
            return Content(cartItemCount.ToString());
        }

        private Dictionary<string, decimal> CalculateTotalAmount(string userId)
        {
            var cartItems = _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .Include(c => c.Product.User) // Include the User navigation property
                .ToList();

            var totalAmounts = new Dictionary<string, decimal>();

            foreach (var cartItem in cartItems)
            {
                if (cartItem.Product != null && cartItem.Product.User != null)
                {
                    var sellerId = cartItem.Product.UserId;

                    if (!totalAmounts.ContainsKey(sellerId))
                    {
                        totalAmounts[sellerId] = 0;
                    }

                    totalAmounts[sellerId] += (decimal)cartItem.Quantity * cartItem.Product.ProductPrice;
                }
            }

            return totalAmounts;
        }




        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, int decreaseQuantity, int increaseQuantity)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var cartItem = await _context.CartItems
                    .Include(c => c.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

                if (cartItem != null)
                {
                    // Decrease quantity
                    if (decreaseQuantity > 0)
                    {
                        cartItem.Quantity = Math.Max(0, decreaseQuantity);
                    }
                    // Increase quantity
                    else if (increaseQuantity > 0)
                    {
                        // Check if the increased quantity is within the available stock
#pragma warning disable CS8602 // Dereference of a possibly null reference.

                        if (cartItem.Quantity + 1 <= cartItem.Product.ProductStock)
                        {
                            cartItem.Quantity++;
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Üzgünüz, stok yetersiz.";
                            return RedirectToAction("Index");
                        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating quantity: {ex.Message}");
                return View("Error");
            }
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            ViewBag.WalletBalance = wallet?.Balance;
            var cartItems = await _context.CartItems
                                    .Where(c => c.UserId == userId)
                                    .Include(c => c.Product)
                                        .ThenInclude(p => p.Images) // Eğer ürün resimleri de gerekliyse bu satırı ekleyin
                                    .ToListAsync();
            var totalAmounts = CalculateTotalAmount(userId);

            var cartModel = new CartViewModel
            {
                CartItems = cartItems,
                TotalAmounts = totalAmounts
            };

            // Calculate the total cart amount
            cartModel.TotalAmount = totalAmounts.Values.Sum();

            // Pass the context data to the view
            ViewData["Context"] = _context;

            return View(cartModel);
        }




    }
}
