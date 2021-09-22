using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Credit { get; set; }
        public int? StoreFrontId { get; set; }

        public virtual StoreFront StoreFront { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
