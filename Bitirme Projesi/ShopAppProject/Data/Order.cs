// Data/Order.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string? UserId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }
        [Display(Name = "Kargo Takip Bilgisi")]
        public string? ShipmentTrackingInfo { get; set; }

        public ApplicationUser? User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
