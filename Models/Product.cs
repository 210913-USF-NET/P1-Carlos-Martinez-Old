using System;

namespace Models
{
    public class Product
    {
        public Product() {}

        // constructor w/ Name
        public Product(string name)
        {
            this.Name = name;
        }

        // properties
        public string Name { get; set; }
        public decimal Price { get; set; } // decimal is used for moneys
        public string Description { get; set; }

        // output string
        public override string ToString()
        {
            return $"Name: {this.Name}";
        }
    }
}