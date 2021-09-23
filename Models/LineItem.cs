namespace Models
{
    public class LineItem
    {
        public LineItem(int OID, int IID)
        {
            this.OrderId = OID;
            this.InventoryId = IID;
        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int InventoryId { get; set; }
    }
}