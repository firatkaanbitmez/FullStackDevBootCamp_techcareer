using ShopAppProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;



namespace ShopAppProject.Controllers
{
    [Authorize(Roles = "Admin,Company")]
    public class myProductController : Controller
    {
        private readonly DataContext _context;

        public myProductController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Check if the user has the "Admin" or "Company" role
            if (User.IsInRole("Admin") || User.IsInRole("Company"))
            {
                var products = await _context.Products
                                .Include(p => p.Images) // Resimleri de yükle
                                .ToListAsync();
                return View(products);
            }
            else
            {
                // Redirect to the home page or another page
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = GetCategorySelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model, List<IFormFile> ImageFiles)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = GetCategorySelectList(); // Re-populate the category list
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get current user ID
            model.UserId = userId; // Assign the user ID to the product

            if (ImageFiles == null || ImageFiles.Count == 0)
            {
                ModelState.AddModelError("ProductImages", "En az bir resim yüklenmelidir.");
                ViewBag.CategoryList = GetCategorySelectList();
                return View(model);
            }

            model.Images = new List<ProductImage>(); // Initialize the Images collection

            foreach (var image in ImageFiles)
            {
                if (image != null && image.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
                    var imagePath = Path.Combine("wwwroot/images", uniqueFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    model.Images.Add(new ProductImage { ImagePath = imagePath });
                }
            }

            _context.Products.Add(model); // Add the product to the context
            await _context.SaveChangesAsync(); // Save changes

            return RedirectToAction("Index"); // Redirect after successful creation
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.Include(p => p.Comments).FirstOrDefault(p => p.ProductId == id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int productId, string content)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
#pragma warning disable CS8602 // Dereference of a possibly null reference.

            var userName = HttpContext.User.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.


            var comment = new Comment
            {
                ProductId = productId,
                UserId = userId,
                UserName = userName,
                Content = content,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = productId });
        }

        public SelectList GetCategorySelectList()
        {
            var categories = new List<SelectListItem>
            {
                new SelectListItem { Value = "Elektronik", Text = "Elektronik" },
                new SelectListItem { Value = "Moda", Text = "Moda" },
                new SelectListItem { Value = "Ev, Yaşam, Kırtasiye, Ofis", Text = "Ev, Yaşam, Kırtasiye, Ofis" },
                new SelectListItem { Value = "Oto, Bahçe, Yapı Market", Text = "Oto, Bahçe, Yapı Market" },
                new SelectListItem { Value = "Anne, Bebek, Oyuncak", Text = "Anne, Bebek, Oyuncak" },
                new SelectListItem { Value = "Spor, Outdoor", Text = "Spor, Outdoor" },
                new SelectListItem { Value = "Kozmetik, Kişisel Bakım", Text = "Kozmetik, Kişisel Bakım" },
                new SelectListItem { Value = "Süpermarket, Pet Shop", Text = "Süpermarket, Pet Shop" },
                new SelectListItem { Value = "Kitap, Müzik, Film, Hobi", Text = "Kitap, Müzik, Film, Hobi" },
                // ... other categories ...
            };

            return new SelectList(categories, "Value", "Text");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = await _context.Products
                                        .Include(p => p.Images)
                                        .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Check if the user is an Admin
            if (!User.IsInRole("Admin") && product.UserId != userId)
            {
                return Forbid(); // If not an Admin and not the owner, deny access
            }

            ViewBag.CategoryList = GetCategorySelectList(); // Set the ViewBag for categories

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product model, int[] deleteImages, IFormFile[] newImages)
        {
            var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }


            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);




            // Check if the user is an Admin
            if (!User.IsInRole("Admin") && product.UserId != userId)
            {
                return Forbid(); // If not an Admin and not the owner, deny access
            }

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = GetCategorySelectList(); // Set the ViewBag for categories
                return View(model);
            }
            // Silme işlemleri
            foreach (var imageId in deleteImages)
            {
                var image = product.Images.FirstOrDefault(i => i.Id == imageId);
                if (image != null)
                {
                    var imagePath = image.ImagePath;
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    _context.ProductImages.Remove(image);
                }
            }

            // Yeni resimleri ekleme
            foreach (var newImage in newImages)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(newImage.FileName);
                var imagePath = Path.Combine("wwwroot/images", uniqueFileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await newImage.CopyToAsync(stream);
                }

                product.Images.Add(new ProductImage { ImagePath = imagePath });
            }
            // Update all properties including the category
            product.ProductTitle = model.ProductTitle;
            product.ProductDesc = model.ProductDesc;
            product.ProductStock = model.ProductStock;
            product.ProductPrice = model.ProductPrice;
            product.ProductImage = model.ProductImage;
            product.ProductCategory = model.ProductCategory; // Update the category

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id, int productId)
        {
            var image = await _context.ProductImages.FirstOrDefaultAsync(i => i.Id == id);
            if (image != null)
            {
                var imagePath = image.ImagePath;
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _context.ProductImages.Remove(image);
                await _context.SaveChangesAsync();
            }

            // Kullanıcıyı aynı ürünün düzenleme sayfasına geri yönlendir
            return RedirectToAction("Edit", new { id = productId });
        }


        public IActionResult DeleteSuccess()
        {
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Check if the user is an Admin or the owner of the product
            if (!User.IsInRole("Admin") && product.UserId != userId)
            {
                return Forbid(); // If not an Admin and not the owner, deny access
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = await _context.Products
                                           .Include(p => p.Images)
                                           .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            // Check if the user is an Admin or the owner of the product
            if (!User.IsInRole("Admin") && product.UserId != userId)
            {
                return Forbid(); // If not an Admin and not the owner, deny access
            }

            // Delete related images and other data

            foreach (var image in product.Images)
            {
                var imagePath = image.ImagePath;
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _context.ProductImages.Remove(image);
            }

            // Remove the product from the context and save changes
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            if (!User.IsInRole("Admin") && product.UserId != userId)
            {
                return RedirectToAction("AccessDenied"); // Add this line
            }

            // Redirect to a success page or another appropriate location
            return RedirectToAction("DeleteSuccess");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
