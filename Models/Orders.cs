using System;
using System.Collections.Generic;

namespace Models
{
    public class Orders
    {
        public Orders() {}
        public Orders(int CID)
        {
            this.CustomerId = CID;
            this.Date = DateTime.Now;
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public int Total { get; set; }
    }
}