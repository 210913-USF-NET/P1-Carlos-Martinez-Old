﻿using System.Collections.Generic;
using Models;


namespace DL
{
    public interface IRepo
    {
        // Stores [Add Store, Get Stores]
        StoreFront AddStoreFront(StoreFront store);
        List<StoreFront> GetAllStoreFronts();
        StoreFront GetStoreFront(int ID);

        // Products [Add Product, Get Products]
        Product AddProduct(Product product);
        List<Product> GetAllProducts();

        // Customers [Add Customer, Get All Customers, Get Specific Customer, Update Customer]
        Customer AddCustomer(Customer cust);
        List<Customer> GetAllCustomers();
        Customer GetCustomer(int name);
        Customer UpdateCustomer(Customer cust);

        // Inventory
        Inventory AddInventory(Inventory inventory);
        List<Inventory> GetInventory(int store);
    }
}