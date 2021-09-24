using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = Models;
using Entity = DL.Entities;
using System.Data.SqlClient;
using System.IO;

namespace DL
{
    public class DBRepo : IRepo
    {
        // dbcontext
        string connectionString = File.ReadAllText(@"../connectionString.txt");
        private Entity.LinguzRevatureStoreContext _context;
        public DBRepo(Entity.LinguzRevatureStoreContext context)
        {
            _context = context;
        }
        Model.Customer IRepo.AddCustomer(Model.Customer custo)
        {
            Entity.Customer custoToAdd = new Entity.Customer(){
                Name = custo.Name,
                Credit = custo.Credit,
                StoreFrontId = -1
            };

            _context.Add(custoToAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return custo;
        }

        List<Model.Customer> IRepo.GetAllCustomers()
        {
            // select * from Customers in SQL Query
            // Gets Entities.Customer
            // and converts to Model.Customer
            return _context.Customers.Select(
                customer => new Model.Customer() {
                    Id = customer.Id,
                    Name = customer.Name,
                    // Orders = customer.Orders,
                    // Inventory = customer.Inventory,
                    // Credit = customer.Credit,
                    // DefaultStore = customer.DefaultStore
                }
            ).ToList();
        }

        public Model.Customer UpdateCustomer(Model.Customer customerToUpdate)
        {
            string queryString = $"UPDATE Customer SET Name = @name, Credit = @money, StoreFrontId = @sfID, hasDefaultStore = @hDS WHERE Id = @cID;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@name", customerToUpdate.Name);
                command.Parameters.AddWithValue("@money", customerToUpdate.Credit);
                command.Parameters.AddWithValue("@sfID", customerToUpdate.StoreFrontID);
                command.Parameters.AddWithValue("@hDS", customerToUpdate.hasDefaultStore);
                command.Parameters.AddWithValue("@cID", customerToUpdate.Id);
                command.ExecuteNonQuery();
            }

            return customerToUpdate;
            /* Entity.Customer custoToUpdate = new Entity.Customer() {
                     Id = customerToUpdate.Id,
                     Name = customerToUpdate.Name,
                     Credit = customerToUpdate.Credit,
                     // Orders = customerToUpdate.Orders,
                     // Inventory = customerToUpdate.Inventory,
                     StoreFrontId = customerToUpdate.StoreFrontID
                 };

             custoToUpdate = _context.Customers.Update(custoToUpdate).Entity;
             _context.SaveChanges();
             _context.ChangeTracker.Clear();

             return new Model.Customer() {
                     Id = custoToUpdate.Id,
                     Name = custoToUpdate.Name,
                     Credit = (int) custoToUpdate.Credit,
                     // Orders = custoToUpdate.Orders,
                     // Inventory = custoToUpdate.Inventory,
                     // StoreFrontID = custoToUpdate.StoreFrontId
                     
              } */
        }

        Model.Product IRepo.AddProduct(Model.Product product)
        {
            Entity.Product prodToAdd = new Entity.Product(){
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };

            _context.Add(prodToAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return product;
        }

        Model.StoreFront IRepo.AddStoreFront(Model.StoreFront store)
        {
            Entity.StoreFront storeToAdd = new Entity.StoreFront(){
                Name = store.Name,
                // Inventories = store.Inventories
            };

            _context.Add(storeToAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return store;
        }
        public Model.StoreFront GetStoreFront(int ID)
        {
            // Can I just call the get all customers method?
            List<Model.StoreFront> allStores = _context.StoreFronts.Select(
                store => new Model.StoreFront() {
                    Id = store.Id,
                    Name = store.Name,
                    // Orders = customer.Orders,
                    // Inventory = customer.Inventory,
                    // Credit = customer.Credit,
                    // DefaultStore = customer.DefaultStore
                }
            ).ToList();

            foreach (Model.StoreFront check in allStores)
            {
                if (check.Id == ID)
                {
                    return check;
                }
            }
            return null;
        }

        List<Model.Product> IRepo.GetAllProducts()
        {
            return _context.Products.Select(
                product => new Model.Product() {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                }
            ).ToList();
        }

        List<Model.StoreFront> IRepo.GetAllStoreFronts()
        {
            return _context.StoreFronts.Select(
                store => new Model.StoreFront() {
                    Id = store.Id,
                    Name = store.Name
                }
            ).ToList();
        }

        Model.Customer IRepo.GetCustomer(int ID)
        {
            // Can I just call the get all customers method?
            List<Model.Customer> allCustos = _context.Customers.Select(
                customer => new Model.Customer() {
                    Id = customer.Id,
                    Name = customer.Name,
                    // Orders = customer.Orders,
                    // Inventory = customer.Inventory,
                    Credit = (int) customer.Credit,
                    StoreFrontID = (int) customer.StoreFrontId,
                    hasDefaultStore = (int) customer.HasDefaultStore
                }
            ).ToList();

            foreach (Model.Customer check in allCustos)
            {
                if (check.Id == ID)
                {
                    return check;
                }
            }
            return null;
        }
    }
}