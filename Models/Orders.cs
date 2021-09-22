using System;
using System.Collections.Generic;

namespace Models
{
    public class Orders
    {
        public int Id { get; set; }
        // properties
        public List<LineItem> LineItems { get; set; }
        public int Total { get; set; }
    }
}