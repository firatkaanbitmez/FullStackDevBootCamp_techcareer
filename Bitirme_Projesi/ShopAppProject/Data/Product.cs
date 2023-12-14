//Data/Product.cs

using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDesc { get; set; }
        public string? ProductPrice { get; set; }

        // Foreign key for the user who added the product
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

    }

}