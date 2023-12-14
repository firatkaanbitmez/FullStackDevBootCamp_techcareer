//Data/LoginViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
