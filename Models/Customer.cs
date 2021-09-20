using System;
using System.Collections.Generic;

namespace Models
{
    public class Customer
    {
        // default constructor
        public Customer() 
        {
            // Start every customer out with 30 gold pieces. 
            Credit = 30;

            // Initialize the lists. Set the default store to null. 
            Orders = new List<Orders>();
            Inventory = new List<Inventory>();
            defaultStore = new StoreFront();
        }

        // constructor w/ Name
        public Customer(string name) : this()
        {
            this.Name = name;
        }

        // properties
        public string Name { get; set; }
        public List<Orders> Orders { get; set; }
        public List<Inventory> Inventory { get; set; }
        public int Credit { get; set; }
        public StoreFront defaultStore { get; set; }
    }
}