using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Product
    {
        public Product()
        {
            Inventories = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
