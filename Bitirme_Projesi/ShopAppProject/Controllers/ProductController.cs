
// ProductController.cs
using Microsoft.AspNetCore.Mvc;
using ShopAppProject.Models;
using System.Collections.Generic;

namespace ShopAppProject.Controllers
{
    public class ProductController : Controller
    {
        // Ürün listesini gösteren eylem
        public IActionResult Index()
        {
            // Ürünleri veritabanından al ve view'e gönder
            List<Product> products = new List<Product>(); // Ürünleri almak için servisi veya repository'i kullanın.
            return View(products);
        }

        // Ürün detaylarını gösteren eylem
        public IActionResult Details(int id)
        {
            // Belirli bir ürünün detaylarını al ve view'e gönder
            Product product = new Product(); // Ürünü almak için servisi veya repository'i kullanın.
            return View(product);
        }
    }
}
