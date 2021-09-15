using Models;
using System.Collections.Generic;
using DL;

namespace BL
{
    public interface IBL
    {
        List<Customer> GetAllCustomers();

        Customer AddCustomer(Customer custo);
    }
}
