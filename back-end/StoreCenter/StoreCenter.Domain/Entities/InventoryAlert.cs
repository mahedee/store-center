using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Domain.Entities
{
    // InventoryAlert Entity
    public class InventoryAlert
    {
        public int AlertId { get; set; }
        public int ItemId { get; set; }
        public Product Product { get; set; }
        public int Threshold { get; set; }
        public string AlertMessage { get; set; }
    }
}
