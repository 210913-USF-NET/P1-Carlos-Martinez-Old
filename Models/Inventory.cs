namespace Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public Product Item { get; set; }
        public int Quantity { get; set; }
    }
}