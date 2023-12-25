//Data/CartItem.cs
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopAppProject.Data
{
    public class CartItem
    {

        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public string? UserId { get; set; }
        public int Quantity { get; set; }

        public string? SellerId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
