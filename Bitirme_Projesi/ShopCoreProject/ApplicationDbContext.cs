// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using ShopCoreProject.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSet'leriniz buraya eklenecek
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    // Diğer DbSet'leri ekleyin
}
