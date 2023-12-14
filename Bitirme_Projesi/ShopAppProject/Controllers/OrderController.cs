// Controllers/OrderController.cs

using Microsoft.AspNetCore.Mvc;
using ShopAppProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ShopAppProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly DataContext _context; // Add this line

        public OrderController(DataContext context) // Add this line
        {
            _context = context; // Add this line
        }

        public IActionResult Index()
        {
            // Fetch orders from the database and pass them to the view
            // (Implement this logic based on your requirements)
            var orders = GetOrdersFromDatabase();
            return View(orders);
        }

        // Implement a method to fetch orders from the database
        private List<Order> GetOrdersFromDatabase()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch orders for the current user from the database
            var orders = _context.Orders
                                .Where(o => o.UserId == userId)
                                .ToList();

            return orders;
        }
    }
}
