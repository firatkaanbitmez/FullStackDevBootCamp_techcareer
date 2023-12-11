using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class Customer
    {
        // id => primary key
        public int CustomerId { get; set; }
        public string? CustomerAd { get; set; }
        public string? CustomerSoyad { get; set; }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

    }
}