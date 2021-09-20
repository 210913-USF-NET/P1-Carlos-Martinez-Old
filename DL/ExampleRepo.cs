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

    
        // Products [Add Product, Get Products]
        public Product AddProduct(Product product)
        {
            filePath = "../DL/Products.json";
            
            // get all products
            List<Product> allProducts = GetAllProducts();

            allProducts.Add(product);

            // serialize
            jsonString = JsonSerializer.Serialize(allProducts);

            // Write to file
            File.WriteAllText(filePath, jsonString);

            return product;
        }
        public List<Product> GetAllProducts()
        {
            filePath = "../DL/Products.json";
            jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Product>>(jsonString);
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

        public Customer GetCustomer(string name)
        {
            List<Customer> allCustomers = GetAllCustomers();
            if (allCustomers.Count() == 0)
            {
                Console.WriteLine("No customers found.");
                return null;
            }

            foreach (Customer custo in allCustomers)
            {
                if (custo.Name.Equals(name))
                {
                    return custo;
                }
            }
            return null;
        }
    }
}