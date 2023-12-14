//Data/Order.cs

using System.ComponentModel.DataAnnotations;
using System;


namespace ShopAppProject.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }

        // You might want to add other properties as needed
    }


}