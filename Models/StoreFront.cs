using System;
using System.Collections.Generic;

namespace Models
{
    public class StoreFront
    {
        public StoreFront() 
        {
            Inventories = new List<Inventory>();
        }
        
        // constructor w/ Name
        public StoreFront(string name) : this()
        {
            this.Name = name;
        }

        // properties
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Inventory> Inventories { get; set; }
        
        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.Name}";
        }
    }
}