// AccountController.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopAppProject.Models; // Add this line
namespace ShopAppProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // Kullanıcı hesap bilgilerini gösteren eylem
        public IActionResult Index()
        {
            // Kullanıcının hesap bilgilerini al ve view'e gönder
            User user = new User(); /* Kullanıcı bilgilerini almak için servisi veya repository'i kullanın. */
            return View(user);
        }

   

    }
}