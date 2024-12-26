using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Domain.Entities
{
    // Transaction Entity
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int ItemId { get; set; }
        public Product product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string TransactionType { get; set; } // e.g., "Purchase", "Sale", "Adjustment"
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
