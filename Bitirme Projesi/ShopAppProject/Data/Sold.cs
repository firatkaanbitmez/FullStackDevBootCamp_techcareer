// Data/Sold.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class Sold
    {
        [Key]
        public int SoldId { get; set; }

        [Required]
        public int OrderId { get; set; }

        public Order? Order { get; set; }

        [Required]
        public string? SellerId { get; set; }

        public ApplicationUser? Seller { get; set; }

        [Required]
        public string? BuyerId { get; set; }

        public ApplicationUser? Buyer { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SoldDate { get; set; } = DateTime.Now;
        public string? SoldIdLink { get; set; }

        public string? BuyerFirstName { get; set; }
        public string? BuyerLastName { get; set; }
        public string? BuyerAddress { get; set; }
        public string? BuyerPhoneNumber { get; set; }
        public string? ShipmentTrackingInfo { get; set; }

    }
}
