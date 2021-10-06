using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using System.IO;

namespace DL
{
    public class DBRepo : IRepo
    {
        // dbcontext
        private ElephantDBContext _context;
        public DBRepo(ElephantDBContext context)
        {
            _context = context;
        }

        // [[CUSTOMERS]]
        public Customer AddCustomer(Customer custo)
        {
            /// <summary>
            /// Adds a new customer to the Database, then saves the Database. 
            /// </summary>
            custo.Credit = 30;
            custo = _context.Add(custo).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return custo;
        }
        public List<Customer> GetAllCustomers()
        {
            /// <summary>
            /// Returns every customer available to the system. 
            /// Other models (GetCustomer) can be used to get other information. 
            /// </summary>
            /// <returns>list of customers with just name and ID</returns>
            
            return _context.Customers.Select(
                customer => new Customer()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Credit = customer.Credit,
                    Password = customer.Password
                }
            ).ToList();
        }
        public Customer UpdateCustomer(Customer customerToUpdate)
        {
            /// <summary>
            /// Gets a customer object and replaces the DB's customer of the same ID
            /// with the new customer object. 
            /// </summary>
            /// <param name="c.Id">c.Id is the ID of the customer being updated</param>
            /// <returns>the updated customer</returns>
            /// 
            Customer updatedCust = new Customer()
            {
                Id = customerToUpdate.Id,
                Name = customerToUpdate.Name,
                Credit = customerToUpdate.Credit
            };

            updatedCust = _context.Customers.Update(updatedCust).Entity;
            _context.SaveChanges();

            return customerToUpdate;
        }
        public Customer GetCustomer(int ID)
        {
            /// <summary>
            /// Gets a specific customer, as references by ID. 
            /// </summary>
            /// <returns>returns the customer with the ID you want, or null if none found.</returns>
            List<Customer> allCustos = _context.Customers.Select(
                customer => new Customer()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Credit = (int)customer.Credit,
                    StoreFrontID = (int)customer.StoreFrontID,
                    hasDefaultStore = (int)customer.hasDefaultStore
                }
            ).ToList();

            foreach (Customer check in allCustos)
            {
                if (check.Id == ID)
                {
                    return check;
                }
            }
            return null;
        }
        public void RemoveCustomer(int Id)
        {
            _context.Customers.Remove(GetCustomer(Id));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        // [PRODUCTS]
        public Product AddProduct(Product product)
        {
            /// <summary>
            /// Adds a product and saves it to the DB. 
            /// </summary>
            /// <returns>returns the product added.</returns>
            Product prodToAdd = new Product()
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
        public List<Product> GetAllProducts()
        {
            throw new NotImplementedException();
            ///// <summary>
            ///// Gets all the products from the DB. 
            ///// </summary>
            ///// <returns>returns a list of all products</returns>
            //return _context.Products.Select(
            //    product => new Product()
            //    {
            //        Name = product.Name,
            //        Price = product.Price,
            //        Description = product.Description
            //    }
            //).ToList();
        }

        // [[STOREFRONTS]]
        public StoreFront AddStoreFront(StoreFront store)
        {
            /// <summary>
            /// Adds a storefront and saves it to the DB
            /// </summary>
            /// <returns>the new storefront</returns>
            StoreFront storeToAdd = new StoreFront()
            {
                Name = store.Name
            };

            _context.Add(storeToAdd);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return store;
        }
        public StoreFront GetStoreFront(int ID)
        {
            /// <summary>
            /// Gets a specific storefront. 
            /// </summary>
            /// <returns>Returns the storefront which matches the ID provided</returns>
            List<StoreFront> allStores = _context.StoreFronts.Select(
                store => new StoreFront()
                {
                    Id = store.Id,
                    Name = store.Name
                }
            ).ToList();

            foreach (StoreFront check in allStores)
            {
                if (check.Id == ID)
                {
                    return check;
                }
            }
            return null;
        }
        public List<StoreFront> GetAllStoreFronts()
        {
            /// <summary>
            /// Returns all the storefronts. 
            /// </summary>
            return _context.StoreFronts.Select(
                store => new StoreFront()
                {
                    Id = store.Id,
                    Name = store.Name
                }
            ).ToList();
        }
        public StoreFront UpdateStore(StoreFront storeToUpdate)
        {
            /// <summary>
            /// Gets a customer object and replaces the DB's customer of the same ID
            /// with the new customer object. 
            /// </summary>
            /// <param name="c.Id">c.Id is the ID of the customer being updated</param>
            /// <returns>the updated customer</returns>
            /// 
            StoreFront updatedStore = new StoreFront()
            {
                Id = storeToUpdate.Id,
                Name = storeToUpdate.Name
            };

            updatedStore = _context.StoreFronts.Update(updatedStore).Entity;
            _context.SaveChanges();

            return storeToUpdate;
        }

        public void RemoveStore(int Id)
        {
            _context.StoreFronts.Remove(GetStoreFront(Id));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        // [[INVENTORY]]
        public Inventory AddInventory(Inventory inventory)
        {
            throw new NotImplementedException();
            ///// <summary>
            ///// Adds inventory to the DB. 
            ///// </summary>
            //Inventory invToAdd = new Inventory()
            //{
            //    StoreId = inventory.StoreFrontId,
            //    ProductId = inventory.ProductId,
            //    Quantity = inventory.Quantity
            //};

            //List<Inventory> storeInventory = GetInventory(inventory.StoreFrontId);

            //int check = 0;
            //foreach (Inventory item in storeInventory)
            //{
            //    if (item.ProductId == invToAdd.ProductId)
            //    {
            //        check = item.Quantity + invToAdd.Quantity;
            //    }
            //}

            //// Update the quantity in the DB instead of adding a brand new one. 
            //if (check != 0)
            //{
            //    throw new NotImplementedException();
            ////    // UPDATE
            ////    string queryString = $"UPDATE Inventory SET Quantity = @quant WHERE StoreId = @SID AND ProductId = @PID;";
            ////    using (SqlConnection connection = new SqlConnection(connectionString))
            ////    {
            ////        connection.Open();
            ////        SqlCommand command = new SqlCommand(queryString, connection);
            ////        command.Parameters.AddWithValue("@quant", check);
            ////        command.Parameters.AddWithValue("@SID", invToAdd.StoreId);
            ////        command.Parameters.AddWithValue("@PID", invToAdd.ProductId);
            ////        command.ExecuteNonQuery();
            ////        return inventory;
            ////    }
            ////}

            ////_context.Add(invToAdd);
            ////_context.SaveChanges();
            ////_context.ChangeTracker.Clear();

            //return inventory;
        }
        public List<Inventory> GetInventory(int store)
        {
            throw new NotImplementedException();
            ///// <summary>
            ///// Get all inventories, get all products,
            ///// returns a new inventory list of the inventories
            ///// that are within the store ID provided
            ///// </summary>
            ///// <returns>a full list of the store inventory</returns>
            //List<Inventory> allInventories = _context.Inventories.Select(
            //    inventory => new Inventory()
            //    {
            //        Id = inventory.Id,
            //        ProductId = inventory.ProductId,
            //        StoreFrontId = inventory.StoreId,
            //        Quantity = inventory.Quantity
            //    }
            //        ).ToList();

            //List<Product> allProducts = _context.Products.Select(
            //            product => new Product()
            //            {
            //                Id = product.Id,
            //                Name = product.Name,
            //                Price = product.Price,
            //                Description = product.Description
            //            }
            //        ).ToList();

            //List<Inventory> storeInventory = new List<Inventory>();

            //var temp = from m1 in allInventories
            //           join m2 in allProducts on m1.ProductId equals m2.Id
            //           select new { m1.Id, m1.ProductId, m2.Name, m1.StoreFrontId, m1.Quantity };

            //foreach (var item in temp)
            //{
            //    if (item.StoreFrontId == store)
            //    {
            //        Inventory newEntry = new Inventory();
            //        newEntry.Id = item.Id;
            //        newEntry.ProductId = item.ProductId;
            //        newEntry.StoreFrontId = item.StoreFrontId;
            //        newEntry.Product = item.Name;
            //        newEntry.Quantity = item.Quantity;
            //        storeInventory.Add(newEntry);
            //    }
            //}

            //return storeInventory;
        }
        public List<Inventory> UpdateInventory(List<Inventory> inventoryToUpdate)
        {
            throw new NotImplementedException();
            /// <summary>
            /// Replaces the inventory in the DB with the updated
            /// inventory provided. 
            /// </summary>

            //foreach (Inventory item in inventoryToUpdate)
            //{
            //    Inventory updatedInventory = (from i in _context.Inventories
            //                                           where i.Id == item.Id
            //                                           select i).SingleOrDefault();

            //    updatedInventory.Quantity = item.Quantity;
            //}

            //_context.SaveChanges();

            //return inventoryToUpdate;
        }

        // [[ORDERS]]
        public List<Orders> GetAllOrders()
        {
            throw new NotImplementedException();
            /// <summary>
            /// Returns all the orders. 
            /// </summary>
            //return _context.Orders.Select(
            //    order => new Orders()
            //    {
            //        Id = order.Id,
            //        CustomerId = order.CustomerId,
            //        StoreFrontId = order.StoreFrontId,
            //        Date = order.Date,
            //        Total = (int)order.Total
            //    }
            //).ToList();
        }
        public Orders AddOrder(Orders order)
        {
            throw new NotImplementedException();
            ///// <summary>
            ///// Adds an Order to the DB. 
            ///// </summary>
            //Order orderToAdd = new Order()
            //{
            //    CustomerId = order.CustomerId,
            //    StoreFrontId = order.StoreFrontId,
            //    Date = order.Date,
            //    Total = order.Total
            //};

            //_context.Add(orderToAdd);
            //_context.SaveChanges();
            //_context.ChangeTracker.Clear();

            //return order;
        }
        public List<Orders> getOrderHistory(int custoId)
        {
            /// <summary>
            /// Gets all the orders which have a specific CustomerID
            /// </summary>
            List<Orders> allOrders = GetAllOrders();
            List<Orders> custoOrders = new List<Orders>();
            
            foreach (Orders order in allOrders)
            {
                if (order.CustomerId == custoId)
                {
                    custoOrders.Add(order);
                }
            }

            return custoOrders;
        }

        // [[LINE ITEMS]]
        public List<LineItem> AddLineItem(List<LineItem> lineitemList)
        {
            throw new NotImplementedException();
            ///// <summary>
            ///// Adds each item in a list of line items to the DB. 
            ///// </summary>
            //List<LineItem> linesToAdd = new List<LineItem>();
            
            //foreach (LineItem lineitem in lineitemList)
            //{
            //    LineItem lineitemToAdd = new LineItem(){
            //        Id = lineitem.Id,
            //        OrderId = lineitem.OrderId,
            //        ProductId = lineitem.ProductId,
            //        Quantity = lineitem.Quantity
            //    };

            //    linesToAdd.Add(lineitemToAdd);
            //}

            //_context.LineItems.AddRange(linesToAdd);
            //_context.SaveChanges();
            //_context.ChangeTracker.Clear();
            //return lineitemList;
        }
        public List<LineItem> GetLineItembyOrderID(int ID)
        {
            throw new NotImplementedException();
            ///// <summary>
            ///// Gets all the line items with a specified OrderId and returns a list of them. 
            ///// </summary>
            //return _context.LineItems.Where(i => i.OrderId == ID).Select(
            //    lineitem => new LineItem()
            //    {
            //        OrderId = lineitem.OrderId,
            //        ProductId = lineitem.ProductId,
            //        Quantity = lineitem.Quantity
            //    }
            //).ToList();
        }
    }
}