using ShopAppProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization; // Add this line

namespace ShopAppProject.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly DataContext _context; // Add this line

        public CartController(DataContext context) // Add this line
        {
            _context = context; // Add this line
        }

        public IActionResult Index()
        {
            var cartItems = GetCartItems();

            // Calculate the total amount
            decimal totalAmount = cartItems.Sum(item => (decimal)item.Product.ProductPrice * item.Quantity);

            // Pass both cart items and total amount to the view
            var viewModel = new CartViewModel
            {
                CartItems = cartItems,
                TotalAmount = totalAmount
            };
            TempData["CartItemCount"] = cartItems.Count;
            Console.WriteLine($"TempData[\"CartItemCount\"] = {TempData["CartItemCount"]}");


            return View(viewModel);
        }

        private List<CartItem> GetCartItems()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch the cart items for the current user from the database
            var cartItems = _context.CartItems
                                    .Include(c => c.Product) // Include the Product navigation property
                                    .Where(c => c.UserId == userId)
                                    .ToList();

            return cartItems;
        }
        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the cart item with the specified productId for the current user
            var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

            if (cartItem != null)
            {
                // Remove the cart item from the context and save changes
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            // Redirect back to the cart page
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int newQuantity)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the cart item with the specified productId for the current user
            var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

            if (cartItem != null)
            {
                // Update the quantity and save changes
                cartItem.Quantity = newQuantity;
                _context.SaveChanges();
            }

            // You can return a JSON response if needed
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch the cart items for the current user from the database
            var cartItems = _context.CartItems
                                    .Include(c => c.Product) // Include the Product navigation property
                                    .Where(c => c.UserId == userId)
                                    .ToList();

            // Create orders based on cart items (this is a simplified example)
            foreach (var cartItem in cartItems)
            {
                var order = new Order
                {
                    UserId = userId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    OrderDate = DateTime.Now,
                    // You might want to copy other relevant information
                };

                // Add the order to the context
                _context.Orders.Add(order);
            }

            // Remove all cart items for the user
            _context.CartItems.RemoveRange(cartItems);

            // Save changes to the database
            _context.SaveChanges();

            // You can return a JSON response if needed
            return Json(new { success = true });
        }


    }
}
