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
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var orders = GetOrdersFromDatabase();
            return View(orders);
        }


        private List<Order> GetOrdersFromDatabase()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


            var orders = _context.Orders
                                .Where(o => o.UserId == userId)
                                .ToList();

            return orders;
        }
    }
}
