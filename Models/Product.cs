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

        public Product(string name, decimal Price, string Description) : this(name)
        {
            this.Price = Price;
            this.Description = Description;
        }

        // properties
        public string Name { get; set; }
        public Type type { get; set; }
        public decimal Price { get; set; } // decimal is used for moneys
        public string Description { get; set; }
    }
}