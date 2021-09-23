using System;
using System.Collections.Generic;

namespace Models
{
    public class Orders
    {
        public Orders(int CID, int Total)
        {
            this.CustomerId = CID;
            this.Total = Total;
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Total { get; set; }
    }
}