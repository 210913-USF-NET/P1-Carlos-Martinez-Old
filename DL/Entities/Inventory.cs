using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Inventory
    {
        public Inventory()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual StoreFront Store { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
