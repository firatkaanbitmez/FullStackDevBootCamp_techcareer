using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAppProject.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ShopAppProject.Controllers
{
    public class DealsController : Controller
    {
        private readonly DataContext _context;

        public DealsController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var dealss = await _context.Dealss.ToListAsync();
            return View(dealss);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Deals model)
        {
            if (ModelState.IsValid)
            {

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deals = await _context.Dealss.FindAsync(id);
            if (deals == null)
            {
                return NotFound();
            }

            return View(deals);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Deals model)
        {
            if (id != model.DealsId)
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
                    if (!DealsExists(model.DealsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deals = await _context.Dealss
                .FirstOrDefaultAsync(m => m.DealsId == id);
            if (deals == null)
            {
                return NotFound();
            }

            return View(deals);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deals = await _context.Dealss.FindAsync(id);
            _context.Dealss.Remove(deals);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deals = await _context.Dealss
                .FirstOrDefaultAsync(m => m.DealsId == id);

            if (deals == null)
            {
                return NotFound();
            }

            return View(deals);
        }

        private bool DealsExists(int id)
        {
            return _context.Dealss.Any(e => e.DealsId == id);
        }
    }
}
