using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class LineItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int InventoryId { get; set; }
        public int Quantity { get; set; }

        public virtual Inventory Inventory { get; set; }
        public virtual Order Order { get; set; }
    }
}
