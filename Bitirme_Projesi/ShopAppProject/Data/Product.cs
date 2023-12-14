//Data/Product.cs

using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class Product
    {
        [Key]
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
        public string? ProductImage { get; set; }

        // Foreign key for the user who added the product
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

    }

}