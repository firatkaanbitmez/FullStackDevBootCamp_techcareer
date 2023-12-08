// Controller CartController.cs
using Microsoft.AspNetCore.Mvc;
using ShopCoreProject.Models;

namespace ShopCoreProject.Controllers
{
    public class CartController : Controller
    {
        // Sepeti gösteren eylem
        public IActionResult Index()
        {
            ShoppingCart cart = new ShoppingCart(); // Sepeti almak için servisi veya repository'i kullanın.
            return View(cart);
        }

        // Sepete ürün ekleyen eylem
        public IActionResult AddToCart(int productId)
        {
            // Ürünü sepete eklemek için servisi veya repository'i kullanın.
            // Kullanıcının sepetini güncelleyin.
            return RedirectToAction("Index");
        }

        // Sepetten ürün çıkaran eylem
        public IActionResult RemoveFromCart(int productId)
        {
            // Ürünü sepette bulup çıkarmak için servisi veya repository'i kullanın.
            // Kullanıcının sepetini güncelleyin.
            return RedirectToAction("Index");
        }
    }
}
