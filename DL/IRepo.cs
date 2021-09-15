using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {
        Customer AddCustomer(Customer custo);
        List<Customer> GetAllCustomers();
    }
}
