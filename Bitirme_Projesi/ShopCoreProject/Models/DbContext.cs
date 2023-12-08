//Models DbContext.cs

using Microsoft.EntityFrameworkCore;

namespace ShopCoreProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet'ler, veritabanındaki tablolara karşılık gelir.
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        // Diğer DbSet'ler...

        // Bu metot, DbContext türünden bir nesne oluşturur.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("YourConnectionString");
            }
        }
    }
}
