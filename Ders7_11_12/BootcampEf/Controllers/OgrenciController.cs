using BootcampEf.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootcampEf.Controllers
{
    public class OgrenciController : Controller{

        private readonly DataContext _context;

        public OgrenciController(DataContext context){
            _context = context;
        }


        public async Task<IActionResult> Index(){          
            return View(await _context.Ogrenciler.ToListAsync());
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model){
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id){
            if(id==null){
                return NotFound();
            }
            
            var ogr = await _context.Ogrenciler.FindAsync(id);
            //var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(o => o.OgrenciId == id);

            if(ogr == null){
                return NotFound();
            }

            return View(ogr);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Ogrenci model){

            if(id != model.OgrenciId){
                return NotFound();
            }
            if(ModelState.IsValid){
                try{
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }catch(DbUpdateConcurrencyException){
                    if(!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId)){
                        return NotFound();
                    }else{
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(model);
            
        }

    }
}