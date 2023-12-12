//Data/Product.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAppProject.Data
{
    public class Product
    {
        public int ProductId { get; set; }

        [Display(Name = "Product Title")]
        public string? ProductTitle { get; set; }

        [Display(Name = "Product Description")]
        public string? ProductDesc { get; set; }

        [Display(Name = "Product Serial")]
        public int ProductSerial { get; set; }

        [Display(Name = "Product Price")]
        public float ProductPrice { get; set; }

        [Display(Name = "Product Image")]
        public string? ProductImage { get; set; } // Add this property for the image path or URL
    }
}
