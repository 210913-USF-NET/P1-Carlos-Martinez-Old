using Models;
using System.Collections.Generic;

namespace BL
{
    public interface IBL
    {
        // Stores [Add Store, Get Stores]
        StoreFront AddStoreFront(StoreFront store);
        StoreFront GetStoreFront(int Id);
        List<StoreFront> GetAllStoreFronts();
        StoreFront UpdateStore(StoreFront store);
        void RemoveStore(int Id);

        // Products [Add Product, Get Products]
        Product AddProduct(Product product);
        List<Product> GetAllProducts();
        
        // Customers [Add Customer, Get Customers/Customer, Update Customer]
        Customer AddCustomer(Customer cust);
        List<Customer> GetAllCustomers();
        Customer GetCustomer(int ID);
        Customer UpdateCustomer(Customer cust);
        void RemoveCustomer(int Id);

        // Inventories
        List<Inventory> GetInventory(int store);
        Inventory AddInventory(Inventory inventory);
        List<Inventory> UpdateInventory(List<Inventory> ordersToUpdate);

        // Orders
        List<Orders> GetAllOrders();
        Orders AddOrder(Orders order);
        List<Orders> getOrderHistory(int custoId);
        List<Orders> orderList(int activeCustomerId, string choice);
        List<Orders> storeOrders(int storeOrderId, string choice);

        // Line Items
        List<LineItem> AddLineItem(List<LineItem> lineitem);
        List<LineItem> GetLineItembyOrderID(int ID);

        // Special
        int convertString(string entry, int min, int max);
        int convertString(string entry, int min);
    }
}