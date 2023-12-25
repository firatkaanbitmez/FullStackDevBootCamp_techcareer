using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAppProject.Data;
using System.Linq;
using System.Security.Claims;

[Authorize]
public class MyListController : Controller
{
    private readonly DataContext _context;

    public MyListController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var userProductList = _context.UserProductLists
            .Include(upl => upl.Product)
                                           .ThenInclude(p => p.Images) // Make sure to include images

            .Where(upl => upl.UserId == userId)
            .ToList();

        return View(userProductList);
    }

    [HttpPost]
    public IActionResult RemoveFromList(int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var userProduct = _context.UserProductLists
            .FirstOrDefault(upl => upl.UserId == userId && upl.ProductId == productId);

        if (userProduct != null)
        {
            _context.UserProductLists.Remove(userProduct);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}
