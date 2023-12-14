//Data/CartItem.cs
namespace ShopAppProject.Data
{
    public class CartItem
    {

        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string? UserId { get; set; }
        public int Quantity { get; set; }

        public Product? Product { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
