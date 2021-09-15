using System;
using System.Collections.Generic;

namespace Models
{
    public class Orders
    {
        // properties
        public List<LineItem> LineItems { get; set; }
        public int Total { get; set; }
    }
}