using System;
using System.Collections.Generic;

namespace Models
{
    public class Customer
    {
        // default constructor
        public Customer() {}

        // constructor w/ Name
        public Customer(string name)
        {
            this.Name = name;
        }

        // properties
        public string Name { get; set; }
        public List<Orders> Orders { get; set; }

        // output string
        public override string ToString()
        {
            return $"Name: {this.Name}";
        }
    }
}