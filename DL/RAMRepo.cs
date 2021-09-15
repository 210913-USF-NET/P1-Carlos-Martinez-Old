using System;
using System.Linq;
using System.Collections.Generic;
using Models;

namespace DL
{
    public sealed class RAMRepo : IRepo
    {
        private static RAMRepo _instance;

        private RAMRepo()
        {
            // for testing
            _customers = new List<Customer>()
            {
                new Customer()
                {
                    Name = "Carlos Martinez"
                }
            };
        }

        public static RAMRepo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RAMRepo();
            }
            return _instance;
        }

        private static List<Customer> _customers;

        // set Customers
        public Customer AddCustomer(Customer custo)
        {
            _customers.Add(custo);
            return custo;
        }

        // get Customers
        public List<Customer> GetAllCustomers()
        {
            return _customers;
        }
    }
}