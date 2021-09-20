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

        // Stores [Add Store, Get Stores]
        public StoreFront AddStoreFront(StoreFront store)
        {
            return _repo.AddStoreFront(store);
        }

        public List<StoreFront> GetAllStoreFronts()
        {
            return _repo.GetAllStoreFronts();
        }

        
        // Products [Add Product, Get Products]
        public Product AddProduct(Product product)
        {
            return _repo.AddProduct(product);
        }
        public List<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
        }

        public Customer AddCustomer(Customer cust)
        {
            return _repo.AddCustomer(cust);
        }

        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public Customer GetCustomer(string name)
        {
            return _repo.GetCustomer(name);            
        }
    }
}