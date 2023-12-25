// Data/Wallet.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Add this line for List<Transaction>

namespace ShopAppProject.Data
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string? UserId { get; set; }

        // Navigation property for the ApplicationUser
        public ApplicationUser? User { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
