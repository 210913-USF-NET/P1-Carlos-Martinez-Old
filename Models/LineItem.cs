namespace Models
{
    public class LineItem
    {
        public LineItem(){}
        public LineItem(int OID, int PID, int QNT)
        {
            this.OrderId = OID;
            this.ProductId = PID;
            this.Quantity = QNT;
        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}