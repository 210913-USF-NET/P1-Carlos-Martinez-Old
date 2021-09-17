using System.Collections.Generic;
using Models;


namespace DL
{
    public interface IRepo
    {
        StoreFront AddStoreFront(StoreFront store);
        List<StoreFront> GetAllStoreFronts();
        Customer AddCustomer(Customer cust);
        List<Customer> GetAllCustomers();
        Customer GetCustomer(string name);
    }
}