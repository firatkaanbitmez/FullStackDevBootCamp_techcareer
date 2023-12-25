// Controllers/OrderController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAppProject.Data;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ShopAppProject.Models; // Assuming Order class is in the Models namespace


namespace ShopAppProject.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null || order.UserId != HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (cartItems == null || cartItems.Count == 0)
            {
                // Sepet boşsa, Cart sayfasına yönlendir
                return RedirectToAction("Index", "Cart");
            }

            var groupedCartItems = cartItems.GroupBy(c => c.Product.UserId);

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                foreach (var sellerGroup in groupedCartItems)
                {
                    var order = new Order
                    {
                        UserId = userId,
                        TotalAmount = sellerGroup.Sum(c => c.Quantity * c.Product.ProductPrice),
                        OrderDetails = sellerGroup.Select(c => new OrderDetail
                        {
                            ProductId = c.ProductId,
                            Quantity = c.Quantity,
                            UnitPrice = c.Product.ProductPrice
                        }).ToList()
                    };

                    // Var olan Order örneğini takip etmeyi bırak
                    _context.Entry(order).State = EntityState.Detached;

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    var buyer = await _context.Users.FindAsync(userId);

                    // Cüzdan kontrolü
                    var wallet = await _context.Wallets
                        .Include(w => w.Transactions)
                        .FirstOrDefaultAsync(w => w.UserId == userId);

                    if (wallet != null)
                    {
                        // Cüzdan bakiyesi yeterli mi kontrol et
                        if (wallet.Balance < order.TotalAmount)
                        {
                            // Yeterli bakiye yoksa işlemi gerçekleştirme ve hata mesajı gönder
                            transaction.Rollback();
                            TempData["ErrorMessage"] = "Insufficient funds in the wallet.";
                            return RedirectToAction("Index", "Cart");
                        }

                        // Yeterli bakiye varsa, cüzdan bakiyesinden düş ve işlemi kaydet
                        wallet.Balance -= order.TotalAmount;
                        wallet.Transactions.Add(new Transaction
                        {
                            Date = DateTime.Now,
                            Amount = -order.TotalAmount,
                            Info = "Sipariş Ödeme Ücreti Sipariş No: " + order.OrderId,
                            OrderIdLink = order.OrderId.ToString()
                        });

                        await _context.SaveChangesAsync();
                    }
                    bool soldOperationPerformed = false;


                    foreach (var orderDetail in order.OrderDetails)
                    {
                        var sellerId = orderDetail.Product?.UserId;

                        if (sellerId != null)
                        {
                            var product = await _context.Products
                        .FirstOrDefaultAsync(p => p.ProductId == orderDetail.ProductId);

                            // Check if the product exists and has sufficient stock
                            if (product != null && product.ProductStock >= orderDetail.Quantity)
                            {
                                // Update the product stock
                                product.ProductStock -= orderDetail.Quantity;
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                // Rollback transaction and show error message
                                transaction.Rollback();
                                TempData["ErrorMessage"] = "Insufficient stock for product.";
                                return RedirectToAction("Index", "Cart");
                            }

                            if (!soldOperationPerformed)
                            {
                                var sold = new Sold
                                {
                                    OrderId = order.OrderId,
                                    SellerId = sellerId,
                                    BuyerId = userId,
                                    BuyerFirstName = buyer?.FirstName,
                                    BuyerLastName = buyer?.LastName,
                                    BuyerAddress = buyer?.Address,
                                    BuyerPhoneNumber = buyer?.PhoneNumber
                                };

                                // Var olan Sold örneğini takip etmeyi bırak
                                _context.Entry(sold).State = EntityState.Detached;

                                _context.Solds.Add(sold);


                                sold.SoldDate = DateTime.Now;
                                sold.SoldId = sold.SoldId; // Ensure that SoldId is updated in the context
                                sold.SoldIdLink = sold.SoldId.ToString();

                                await _context.SaveChangesAsync();
                                // Satıcı ödeme işlemi
                                var sellerWallet = await _context.Wallets
                                    .Include(w => w.Transactions)
                                    .FirstOrDefaultAsync(w => w.UserId == sellerId);

                                if (sellerWallet != null)
                                {
                                    sellerWallet.Balance += sellerGroup.Sum(c => c.Quantity * c.Product.ProductPrice);
                                    sellerWallet.Transactions.Add(new Transaction
                                    {
                                        Date = DateTime.Now,
                                        Amount = sellerGroup.Sum(c => c.Quantity * c.Product.ProductPrice),
                                        Info = "Ödeme Alındı - Satış No: " + sold.SoldId,
                                        SoldIdLink = sold.SoldId.ToString()
                                    });

                                    await _context.SaveChangesAsync();
                                }
                                // Mark that the sold operation has been performed for this seller group
                                soldOperationPerformed = true;
                            }
                        }
                    }

                    // Sepeti temizle
                    _context.CartItems.RemoveRange(sellerGroup);
                    await _context.SaveChangesAsync();
                }

                // Tüm işlemler başarılı oldu, işlemi tamamla
                transaction.Commit();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Veritabanı hatası, işlemi geri al ve hata sayfasına yönlendir
                transaction.Rollback();
                // Hata sayfasına yönlendir...
                return RedirectToAction("Index", "Error");
            }
            catch (Exception ex)
            {
                // Genel bir hata durumu, işlemi geri al ve hata sayfasına yönlendir
                transaction.Rollback();
                // Hata sayfasına yönlendir...
                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Order");
        }




    }
}