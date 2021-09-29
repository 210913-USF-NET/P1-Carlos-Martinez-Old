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

        // [[CUSTOMERS]]
        public Model.Customer AddCustomer(Model.Customer custo)
        {
            /// <summary>
            /// Adds a new customer to the Database, then saves the Database. 
            /// </summary>
            
            Entity.Customer custoToAdd = new Entity.Customer()
            {
                Name = custo.Name,
                Credit = custo.Credit
            };

            _context.Add(custoToAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return custo;
        }
        public List<Model.Customer> GetAllCustomers()
        {
            /// <summary>
            /// Returns every customer available to the system. 
            /// Other models (GetCustomer) can be used to get other information. 
            /// </summary>
            /// <returns>list of customers with just name and ID</returns>
            
            return _context.Customers.Select(
                customer => new Model.Customer()
                {
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
            /// <summary>
            /// Gets a customer object and replaces the DB's customer of the same ID
            /// with the new customer object. 
            /// </summary>
            /// <param name="c.Id">c.Id is the ID of the customer being updated</param>
            /// <returns>the updated customer</returns>
            Entities.Customer updatedCust = (from c in _context.Customers
                                             where c.Id == customerToUpdate.Id
                                             select c).SingleOrDefault();

            updatedCust.Credit = customerToUpdate.Credit;
            updatedCust.HasDefaultStore = customerToUpdate.hasDefaultStore;
            updatedCust.StoreFrontId = customerToUpdate.StoreFrontID;

            _context.SaveChanges();

            return customerToUpdate;
        }
        public Model.Customer GetCustomer(int ID)
        {
            /// <summary>
            /// Gets a specific customer, as references by ID. 
            /// </summary>
            /// <returns>returns the customer with the ID you want, or null if none found.</returns>
            List<Model.Customer> allCustos = _context.Customers.Select(
                customer => new Model.Customer()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    // Orders = customer.Orders,
                    // Inventory = customer.Inventory,
                    Credit = (int)customer.Credit,
                    StoreFrontID = (int)customer.StoreFrontId,
                    hasDefaultStore = (int)customer.HasDefaultStore
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

        // [PRODUCTS]
        public Model.Product AddProduct(Model.Product product)
        {
            /// <summary>
            /// Adds a product and saves it to the DB. 
            /// </summary>
            /// <returns>returns the product added.</returns>
            Entity.Product prodToAdd = new Entity.Product()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };

            _context.Add(prodToAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return product;
        }
        public List<Model.Product> GetAllProducts()
        {
            /// <summary>
            /// Gets all the products from the DB. 
            /// </summary>
            /// <returns>returns a list of all products</returns>
            return _context.Products.Select(
                product => new Model.Product()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                }
            ).ToList();
        }

        // [[STOREFRONTS]]
        public Model.StoreFront AddStoreFront(Model.StoreFront store)
        {
            /// <summary>
            /// Adds a storefront and saves it to the DB
            /// </summary>
            /// <returns>the new storefront</returns>
            Entity.StoreFront storeToAdd = new Entity.StoreFront()
            {
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
            /// <summary>
            /// Gets a specific storefront. 
            /// </summary>
            /// <returns>Returns the storefront which matches the ID provided</returns>
            List<Model.StoreFront> allStores = _context.StoreFronts.Select(
                store => new Model.StoreFront()
                {
                    Id = store.Id,
                    Name = store.Name
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
        public List<Model.StoreFront> GetAllStoreFronts()
        {
            /// <summary>
            /// Returns all the storefronts. 
            /// </summary>
            return _context.StoreFronts.Select(
                store => new Model.StoreFront()
                {
                    Id = store.Id,
                    Name = store.Name
                }
            ).ToList();
        }

        // [[INVENTORY]]
        public Model.Inventory AddInventory(Model.Inventory inventory)
        {
            /// <summary>
            /// Adds inventory to the DB. 
            /// </summary>
            Entity.Inventory invToAdd = new Entity.Inventory()
            {
                StoreId = inventory.StoreFrontId,
                ProductId = inventory.ProductId,
                Quantity = inventory.Quantity
            };

            List<Model.Inventory> storeInventory = GetInventory(inventory.StoreFrontId);

            int check = 0;
            foreach (Model.Inventory item in storeInventory)
            {
                if (item.ProductId == invToAdd.ProductId)
                {
                    check = item.Quantity + invToAdd.Quantity;
                }
            }

            // Update the quantity in the DB instead of adding a brand new one. 
            if (check != 0)
            {
                // UPDATE
                string queryString = $"UPDATE Inventory SET Quantity = @quant WHERE StoreId = @SID AND ProductId = @PID;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@quant", check);
                    command.Parameters.AddWithValue("@SID", invToAdd.StoreId);
                    command.Parameters.AddWithValue("@PID", invToAdd.ProductId);
                    command.ExecuteNonQuery();
                    return inventory;
                }
            }

            _context.Add(invToAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return inventory;
        }
        public List<Model.Inventory> GetInventory(int store)
        {
            /// <summary>
            /// Get all inventories, get all products,
            /// returns a new inventory list of the inventories
            /// that are within the store ID provided
            /// </summary>
            /// <returns>a full list of the store inventory</returns>
            List<Model.Inventory> allInventories = _context.Inventories.Select(
                inventory => new Model.Inventory()
                {
                    Id = inventory.Id,
                    ProductId = inventory.ProductId,
                    StoreFrontId = inventory.StoreId,
                    Quantity = inventory.Quantity
                }
                    ).ToList();

            List<Model.Product> allProducts = _context.Products.Select(
                        product => new Model.Product()
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Price = product.Price,
                            Description = product.Description
                        }
                    ).ToList();

            List<Model.Inventory> storeInventory = new List<Model.Inventory>();

            var temp = from m1 in allInventories
                       join m2 in allProducts on m1.ProductId equals m2.Id
                       select new { m1.Id, m1.ProductId, m2.Name, m1.StoreFrontId, m1.Quantity };

            foreach (var item in temp)
            {
                if (item.StoreFrontId == store)
                {
                    Model.Inventory newEntry = new Model.Inventory();
                    newEntry.Id = item.Id;
                    newEntry.ProductId = item.ProductId;
                    newEntry.StoreFrontId = item.StoreFrontId;
                    newEntry.Product = item.Name;
                    newEntry.Quantity = item.Quantity;
                    storeInventory.Add(newEntry);
                }
            }

            return storeInventory;
        }
        public List<Model.Inventory> UpdateInventory(List<Model.Inventory> inventoryToUpdate)
        {
            /// <summary>
            /// Replaces the inventory in the DB with the updated
            /// inventory provided. 
            /// </summary>

            foreach (Model.Inventory item in inventoryToUpdate)
            {
                Entities.Inventory updatedInventory = (from i in _context.Inventories
                                                       where i.Id == item.Id
                                                       select i).SingleOrDefault();

                updatedInventory.Quantity = item.Quantity;
            }

            _context.SaveChanges();

            return inventoryToUpdate;
        }

        // [[ORDERS]]
        public List<Model.Orders> GetAllOrders()
        {
            /// <summary>
            /// Returns all the orders. 
            /// </summary>
            return _context.Orders.Select(
                order => new Model.Orders()
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    StoreFrontId = order.StoreFrontId,
                    Date = order.Date,
                    Total = (int)order.Total
                }
            ).ToList();
        }
        public Model.Orders AddOrder(Model.Orders order)
        {
            /// <summary>
            /// Adds an Order to the DB. 
            /// </summary>
            Entity.Order orderToAdd = new Entity.Order()
            {
                CustomerId = order.CustomerId,
                StoreFrontId = order.StoreFrontId,
                Date = order.Date,
                Total = order.Total
            };

            _context.Add(orderToAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return order;
        }
        public List<Model.Orders> getOrderHistory(int custoId)
        {
            /// <summary>
            /// Gets all the orders which have a specific CustomerID
            /// </summary>
            List<Model.Orders> allOrders = GetAllOrders();
            List<Model.Orders> custoOrders = new List<Model.Orders>();
            
            foreach (Model.Orders order in allOrders)
            {
                if (order.CustomerId == custoId)
                {
                    custoOrders.Add(order);
                }
            }

            return custoOrders;
        }

        // [[LINE ITEMS]]
        public List<Model.LineItem> AddLineItem(List<Model.LineItem> lineitemList)
        {
            /// <summary>
            /// Adds each item in a list of line items to the DB. 
            /// </summary>
            List<Entity.LineItem> linesToAdd = new List<Entity.LineItem>();
            
            foreach (Model.LineItem lineitem in lineitemList)
            {
                Entity.LineItem lineitemToAdd = new Entity.LineItem(){
                    Id = lineitem.Id,
                    OrderId = lineitem.OrderId,
                    ProductId = lineitem.ProductId,
                    Quantity = lineitem.Quantity
                };

                linesToAdd.Add(lineitemToAdd);
            }

            _context.LineItems.AddRange(linesToAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return lineitemList;
        }
        public List<Model.LineItem> GetLineItembyOrderID(int ID)
        {
            /// <summary>
            /// Gets all the line items with a specified OrderId and returns a list of them. 
            /// </summary>
            return _context.LineItems.Where(i => i.OrderId == ID).Select(
                lineitem => new Model.LineItem()
                {
                    OrderId = lineitem.OrderId,
                    ProductId = lineitem.ProductId,
                    Quantity = lineitem.Quantity
                }
            ).ToList();
        }
    }
}