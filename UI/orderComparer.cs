using System;
using System.Collections.Generic;
using Models;

namespace UI
{
    public class orderComparer : Comparer<Orders>
    {
        public override int Compare(Orders x, Orders y)
        {
            // Intentionally not implemented. 
            throw new NotImplementedException();
        }
        public int ComparebyAscDate(Orders x, Orders y)
        {
            if (x.Date.CompareTo(y.Date) != 0)
            {
                return x.Date.CompareTo(y.Date);
            }
            else
            {
                return 0;
            }
        }
        public int ComparebyDesDate(Orders y, Orders x)
        {
            if (x.Date.CompareTo(y.Date) != 0)
            {
                return x.Date.CompareTo(y.Date);
            }
            else
            {
                return 0;
            }
        }
        public int ComparebyAscTotal(Orders x, Orders y)
        {
            if (x.Total.CompareTo(y.Total) != 0)
            {
                return x.Total.CompareTo(y.Total);
            }
            else
            {
                return 0;
            }
        }
        public int ComparebyDesTotal(Orders y, Orders x)
        {
            if (x.Total.CompareTo(y.Total) != 0)
            {
                return x.Total.CompareTo(y.Total);
            }
            else
            {
                return 0;
            }
        }
    }
}