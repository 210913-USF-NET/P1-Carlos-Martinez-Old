using System;
using System.Collections.Generic;

namespace Models
{
    public class StoreFront
    {
        public StoreFront() {}
        
        // constructor w/ Name
        public StoreFront(string name)
        {
            this.Name = name;
        }

        // properties
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Inventory> Inventories { get; set; }

        // output string
        public override string ToString()
        {
            return $"Name: {this.Name}";
        }
    }
}