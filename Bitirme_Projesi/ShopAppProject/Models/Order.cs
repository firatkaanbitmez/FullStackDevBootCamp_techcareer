// Models Order.cs örneği
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Diğer özellikler
        public DateTime OrderDate { get; set; } // Order tarihini içerir.
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Sipariş kalemlerini içerir.

        // Kullanıcı ile ilişkilendirmek için UserId eklenebilir.
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }

        // Kullanıcı ile ilişkilendirmek için UserId eklenebilir.
    }
}
