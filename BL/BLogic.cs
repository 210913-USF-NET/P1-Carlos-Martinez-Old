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

        public Customer GetCustomer(int ID)
        {
            return _repo.GetCustomer(ID);            
        }
        public Customer UpdateCustomer(Customer cust)
        {
            return _repo.UpdateCustomer(cust);
        }
        public Inventory AddInventory(Inventory inventory)
        {
            return _repo.AddInventory(inventory);
        }

        public List<Inventory> GetInventory(int store)
        {
            return _repo.GetInventory(store);
        }
        public List<Inventory> UpdateInventory(List<Inventory> ordersToUpdate)
        {
            return _repo.UpdateInventory(ordersToUpdate);
        }

        public List<Orders> GetAllOrders()
        {
            return _repo.GetAllOrders();
        }

        public Orders AddOrder(Orders order)
        {
            return _repo.AddOrder(order);
        }
        public List<LineItem> AddLineItem(List<LineItem> lineitem)
        {
            return _repo.AddLineItem(lineitem);
        }
    }
}