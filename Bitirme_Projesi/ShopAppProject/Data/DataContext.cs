//Data/DataContext.cs
using Microsoft.EntityFrameworkCore;

namespace ShopAppProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Productler => Set<Product>();
        public DbSet<Kurs> Kurslar => Set<Kurs>();
        public DbSet<Customer> Customerler => Set<Customer>();
        public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();


    }
}