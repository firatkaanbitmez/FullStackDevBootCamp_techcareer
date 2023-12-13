using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BootcampEf.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using BootcampEf.Models;


namespace BootcampEf.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    // [Authorize] // Sadece giriş yapmış kullanıcılar bu sayfaya erişebilir
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        // Eğer kullanıcı giriş yapmışsa, kullanıcının adını ViewBag üzerinden gönder
        if (user != null)
        {
            ViewBag.UserName = user.Email;
        }

        return View();
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
