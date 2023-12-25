// Data/Shipping.cs
using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class Shipping
    {
        [Key]
        public int ShippingId { get; set; }

        [Required]
        public string ShippingMethodName { get; set; }

        [DataType(DataType.Currency)]
        public decimal ShippingCost { get; set; }
    }
}

