namespace Models
{
    public class Inventory
    {
        // Each Inventory is one line in an Inventories object
        
        // constructors
        public Inventory(){}
        public Inventory(int PID, int SID, int Quant)
        {
            this.ProductId = PID;
            this.StoreFrontId = SID;
            this.Quantity = Quant;
        }
        
        public Inventory(string product, int Quant)
        {
            this.Product = product;
            this.Quantity = Quant;
        }

        // properties
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public int StoreFrontId { get; set; }
        public int Quantity { get; set; }
        
        public override string ToString()
        {
            return $"Product: {this.Product}, Quantity: {this.Quantity}";
        }

        public string fullString()
        {
            return $"Id: {this.Id}, Product: {this.ProductId}, StoreFrontId: {this.StoreFrontId}, Quantity: {this.Quantity}";
        }
    }
}