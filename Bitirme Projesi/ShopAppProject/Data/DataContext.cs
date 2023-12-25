// Data/DataContext.cs
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopAppProject.Models; // Assuming User class is in the Models namespace


namespace ShopAppProject.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity tablolarını ekleyin
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            builder.Entity<IdentityRole>().ToTable("AspNetRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
            builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");

            builder.Entity<Order>()
    .HasOne(o => o.User)  // Update this to reference the User property in the Order entity
    .WithMany()
    .HasForeignKey(o => o.UserId);

        }



        public DbSet<Product> Products { get; set; }
        public DbSet<Product> Productler => Set<Product>();
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<UserProductList> UserProductLists { get; set; }
        public DbSet<Deals> Dealss { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Sold> Solds { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Comment> Comments { get; set; }

    }
}
