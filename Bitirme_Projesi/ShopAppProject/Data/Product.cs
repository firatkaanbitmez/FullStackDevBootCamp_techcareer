//Data/Product.cs

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAppProject.Data
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductTitle { get; set; }
        public string? ProductDesc { get; set; }
        public int ProductSerial { get; set; }
        public float ProductPrice { get; set; }

    }
}