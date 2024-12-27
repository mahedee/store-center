using StoreCenter.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Domain.Entities
{
    // Customer Entity
    public class Customer : BaseEntity<Guid>
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
