namespace Models
{
    public class Inventory
    {
        // constructors
        public Inventory(int PID, int SID, int Quant)
        {
            this.ProductId = PID;
            this.StoreFrontId = SID;
            this.Quantity = Quant;
        }

        // properties
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StoreFrontId { get; set; }
        public int Quantity { get; set; }
    }
}