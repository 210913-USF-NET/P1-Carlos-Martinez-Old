using System;
using Models;
using System.Collections.Generic;
using DL;

namespace BL
{
    public class BLogic : IBL
    {
        private IRepo _repo;

        public BLogic(IRepo repo)
        {
            _repo = repo;
        }

        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public Customer AddCustomer(Customer custo)
        {
            return _repo.AddCustomer(custo);
        }
    }
}