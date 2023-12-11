using ShopAppProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShopAppProject.Controllers
{
    public class CustomerController : Controller
    {

        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Customerler.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer model)
        {
            _context.Customerler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogr = await _context.Customerler.FindAsync(id);
            //var ogr = await _context.Customerler.FirstOrDefaultAsync(o => o.CustomerId == id);

            if (ogr == null)
            {
                return NotFound();
            }

            return View(ogr);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Customer model)
        {

            if (id != model.CustomerId)
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
                    if (!_context.Customerler.Any(o => o.CustomerId == model.CustomerId))
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