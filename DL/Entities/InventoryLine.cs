using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class InventoryLine
    {
        public InventoryLine()
        {
            Inventories = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
