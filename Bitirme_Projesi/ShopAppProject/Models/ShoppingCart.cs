// Models ShoppingCart.cs örneği
//Models ShoppingCart.cs


using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        // Diğer özellikler

        // Kullanıcı ile ilişkilendirmek için UserId eklenebilir.
        public decimal TotalPrice => Items.Sum(item => item.Product?.Price * item.Quantity ?? 0);
    }

    public class CartItem
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }

        // Kullanıcı ile ilişkilendirmek için UserId eklenebilir.
    }
}
