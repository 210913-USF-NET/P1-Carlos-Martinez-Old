using Models;
using System.Collections.Generic;
using DL;

namespace BL
{
    public interface IBL
    {
        StoreFront AddStoreFront(StoreFront store);
        List<StoreFront> GetAllStoreFronts();
        Customer AddCustomer(Customer cust);
        List<Customer> GetAllCustomers();
    }
}