namespace Models
{
    public class LineItem
    {
        public LineItem(int OID, int IID, int QNT)
        {
            this.OrderId = OID;
            this.InventoryId = IID;
            this.Quantity = QNT;
        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
    }
}