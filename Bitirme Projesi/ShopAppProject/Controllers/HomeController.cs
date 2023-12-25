using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ShopAppProject.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ShopAppProject.Models;
using Microsoft.EntityFrameworkCore; // Bu satırı ekleyin



namespace ShopAppProject.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly DataContext _context;
    public HomeController(
       UserManager<ApplicationUser> userManager,
       SignInManager<ApplicationUser> signInManager,
       DataContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }
    // [Authorize] // Sadece giriş yapmış kullanıcılar bu sayfaya erişebilir
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            ViewBag.UserName = user.Email;
        }

        // Ürünleri ve ilişkili resimleri çek
        var allProducts = await _context.Products
                                        .Include(p => p.Images) // Bu satır önemli
                                        .ToListAsync();

        var random = new Random();
        var shuffledProducts = allProducts.OrderBy(p => random.Next()).Take(5).ToList();

        ViewData["randomProducts"] = shuffledProducts;
        return View();
    }

    public IActionResult GetRandomProducts()
    {
        // Fetch all products from the database
        var allProducts = _context.Products.ToList();

        // Shuffle the products using a random seed
        var random = new Random();
        var shuffledProducts = allProducts.OrderBy(p => random.Next()).Take(4).ToList();

        return PartialView("_RandomProductsPartial", shuffledProducts);
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
