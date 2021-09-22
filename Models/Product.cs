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

        public Product(string name, int Price, string Description) : this(name)
        {
            this.Price = Price;
            this.Description = Description;
        }

        // properties
        public int Id { get; set; }
        public string Name { get; set; }
        public Type type { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}