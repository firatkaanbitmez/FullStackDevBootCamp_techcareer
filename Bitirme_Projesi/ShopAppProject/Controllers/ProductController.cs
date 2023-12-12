//Controllers/ProductControllers.cs
using ShopAppProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShopAppProject.Controllers
{
    public class ProductController : Controller
    {

        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Productler.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            _context.Productler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogr = await _context.Productler.FindAsync(id);
            //var ogr = await _context.Productler.FirstOrDefaultAsync(o => o.ProductId == id);

            if (ogr == null)
            {
                return NotFound();
            }

            return View(ogr);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Product model)
        {

            if (id != model.ProductId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Productler.Any(o => o.ProductId == model.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(model);

        }

    }
}