// OrderController.cs
using Microsoft.AspNetCore.Mvc;
using ShopCoreProject.Models;
using System.Collections.Generic;

namespace ShopCoreProject.Controllers
{
    public class OrderController : Controller
    {
        // Siparişleri gösteren eylem
        public IActionResult Index()
        {
            // Kullanıcının geçmiş siparişlerini al ve view'e gönder
            List<Order> orders = new List<Order>(); // Siparişleri almak için servisi veya repository'i kullanın.
            return View(orders);
        }

        // Sipariş detaylarını gösteren eylem
        public IActionResult Details(int orderId)
        {
            // Belirli bir siparişin detaylarını al ve view'e gönder
            Order order = new Order(); // Siparişi almak için servisi veya repository'i kullanın.
            return View(order);
        }
    }
}