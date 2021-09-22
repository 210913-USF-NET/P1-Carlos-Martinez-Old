using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class StoreFront
    {
        public StoreFront()
        {
            Customers = new HashSet<Customer>();
            Inventories = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
