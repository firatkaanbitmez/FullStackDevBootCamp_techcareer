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

        [Display(Name = "Product Stock")]
        public int ProductStock { get; set; }

        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; } // Change the data type to decimal

        [Display(Name = "Product Category")]
        public string? ProductCategory { get; set; } // Add this line for category

        [Display(Name = "Product Image")]
        public string? ProductImage { get; set; }

        public virtual ICollection<ProductImage>? Images { get; set; }

        public ICollection<UserProductList>? UserProductLists { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<Comment>? Comments { get; set; }


    }
}
