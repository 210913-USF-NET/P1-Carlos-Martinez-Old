namespace Models
{
    public class joinedInventory
    {  
        public joinedInventory() {}
        public joinedInventory(string Name, int Quantity, int Price, string Description)
        {
            this.Name = Name;
            this.Quantity = Quantity;
            this.Price = Price;
            this.Description = Description;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        
    }
}