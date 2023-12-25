// Data/Transaction.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Add this line for List<Transaction>

namespace ShopAppProject.Data
{

    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        // Other properties related to the transaction
        public string? Info { get; set; } // Örneğin, işlemin detayları, türü, vb.
        public string? OrderIdLink { get; set; }

        public string? SoldIdLink { get; set; }
        public int WalletId { get; set; }
        public Wallet? Wallet { get; set; }
    }
}
