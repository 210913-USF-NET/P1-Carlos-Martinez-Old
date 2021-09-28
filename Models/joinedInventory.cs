namespace Models
{
    public class joinedInventory
    {  
        public joinedInventory() {}
        public joinedInventory(joinedInventory old) 
        {
            this.Id = old.Id;
            this.Name = old.Name;
            this.Quantity = old.Quantity;
            this.Price = old.Price;
            this.Description = old.Description;
        }
        public joinedInventory(string Name, int Price, string Description)
        {
            this.Name = Name;
            this.Price = Price;
            this.Description = Description;
        }
        public joinedInventory(int Id, string Name, int Quantity, int Price, string Description)
        {
            this.Id = Id;
            this.Name = Name;
            this.Quantity = Quantity;
            this.Price = Price;
            this.Description = Description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        
        public string fullString()
        {
            return $"Name: {this.Name}";
        }
    }
}