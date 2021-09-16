using System.Collections.Generic;
using Models;
using System.IO;
using System.Text.Json;
using System;
using System.Linq;

namespace DL
{
    public class ExampleRepo : IRepo
    {
        // This is currently a file Repo, to allow testing and the like. 

        // logic to get data
        private string filePath = "";
        private string jsonString;

        public StoreFront AddStoreFront(StoreFront store) 
        {
            filePath = "../DL/Stores.json";

            // get all stores
            List<StoreFront> allStores = GetAllStoreFronts();

            allStores.Add(store);

            // serialize
            jsonString = JsonSerializer.Serialize(allStores);

            // Write to file
            File.WriteAllText(filePath, jsonString);

            return store;
        }

        public List<StoreFront> GetAllStoreFronts()
        {
            filePath = "../DL/Stores.json";
            jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<StoreFront>>(jsonString);
        }

        public Customer AddCustomer(Customer cust)
        {
            filePath = "../DL/Customers.json";

            // get all customers
            List<Customer> allCustomers = GetAllCustomers();

            allCustomers.Add(cust);

            // serialize
            jsonString = JsonSerializer.Serialize(allCustomers);

            // Write to file
            File.WriteAllText(filePath, jsonString);

            return cust;
        }

        public List<Customer> GetAllCustomers()
        {
            filePath = "../DL/Customers.json";
            jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Customer>>(jsonString);
        }
    }
}