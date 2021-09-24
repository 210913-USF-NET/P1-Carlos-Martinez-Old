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
        }

        // constructor w/ Name
        public Customer(string name) : this()
        {
            this.Name = name;
        }

        // properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public int StoreFrontID { get; set; }
        public int hasDefaultStore { get; set; }
        
        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.Name}, Credit: {this.Credit}, StoreFrontID: {this.StoreFrontID}, hasDefaultStore: {this.hasDefaultStore}";
        }
    }
}