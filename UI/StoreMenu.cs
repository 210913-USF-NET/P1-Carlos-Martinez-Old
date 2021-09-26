using BL;
using Models;
using System;
using System.Collections.Generic;  
using System.Linq;

namespace UI
{
    public class storeMenu : IMenu
    {
        private IBL _bl;

        public storeMenu(IBL bl)
        {
            _bl = bl;
        }

        public void Start()
        {
            Console.WriteLine("\nEnter your unique ID: ");
            commonMethods _CMinstance = new commonMethods();
            int input = _CMinstance.convertString(Console.ReadLine(), 0);
            if (input == -1)
            {
                Console.WriteLine("ID not found. Returning to main menu.");
                return;
            }
            Customer custo = _bl.GetCustomer(input);
            if (custo == null)
            {
                Console.WriteLine("ID not found. Returning to main menu.");
                return;
            }
            Console.WriteLine(custo);

            // Check default store. 
            StoreFront activeStore = new StoreFront();
            List<StoreFront> allRestos = _bl.GetAllStoreFronts();
            if (allRestos.Count == 0)
            {
                Console.WriteLine("There are no stores.");
                Console.WriteLine("Please contact management and report this issue.");
                return;
            }
            // What if: Null default store? Ask where they want to go.
            // Else, take them there already. 
            
            if(custo.hasDefaultStore.Equals(1))
            {
                // They have a default store. 
                activeStore = allRestos[custo.StoreFrontID];
            }
            else
            {
                Console.WriteLine("Where would you like to shop?");
                manageStoresMenu _MSMinstance = new manageStoresMenu(_bl);
                activeStore = _MSMinstance.selectStore();
                Console.WriteLine($"Setting default store to {activeStore.Name}");
                custo.StoreFrontID = activeStore.Id;
                custo.hasDefaultStore = 1;
                _bl.UpdateCustomer(custo);
            }

            bool exit = false;
            do
            {
                Console.WriteLine($"\nWelcome to {activeStore.Name}!");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("0- Add Product to Shopping Cart");
                Console.WriteLine("1- Change Store");
                Console.WriteLine("2- Check Shopping Cart");
                Console.WriteLine("x- Exit");
                Console.Write("Input: ");

                switch (Console.ReadLine())
                {
                    case "0": 
                        // Add Product to Order
                        addProductToOrder(activeStore, custo);
                        break; 

                    case "1": 
                        // Change Store
                        Console.WriteLine("Searching for all stores...");

                        manageStoresMenu _instance = new manageStoresMenu(_bl);
                        activeStore = _instance.selectStore();
                        if (activeStore is null)
                        {
                            Console.WriteLine("There are no stores.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Store changed to {activeStore.Name}");
                            custo.StoreFrontID = activeStore.Id;
                            _bl.UpdateCustomer(custo);
                            break;
                        }

                    case "2": 
                        // Check Order
                          // Checkout, Remove Product from Order
                          // Should I make a switch inside the switch or send it over to a new menu?
                        break; 

                    case "x": 
                        exit = true;
                        break;
                }
            } while(!exit);
        }

        private void addProductToOrder(StoreFront store, Customer custo)
        {
            Orders currentOrder = new Orders(custo.Id);
            int runningTotal = 0;

            // Need to join storeInventory and Product
            List<Inventory> storeInventory = _bl.GetInventory(store.Id);
            List<Product> allProducts = _bl.GetAllProducts();
            List<joinedInventory> joinedInventory = new List<joinedInventory>();
            var tempInventory = from m1 in storeInventory
                join m2 in allProducts on m1.ProductId equals m2.Id
                select new {m2.Name, m1.Quantity, m2.Price, m2.Description};

            // also create a lineitem every time the following loop is active. 
            string input;

            foreach (var item in tempInventory)
            {
                joinedInventory.Add(new joinedInventory(item.Name, item.Price, item.Quantity, item.Description));
            }

            // Display products
              // 'c' returns null, for cancel
              // should I deduct inventory as they go?
                // if they cancel, gotta restock inventory
              // should I deduct inventory at checkout? <<partial to this idea>>
                // need to stop them from buying more than all
            // Get customer input
            // Ask for quantity
              // Check if they have bought more than all. If so, set to buy all. 
            // Create LineItem with information (orderId, inventoryID)
            // Add LineItem to DB and List
            // Ask if they want to checkout or buy more. 
            // Repeat if buy more.
            // Else, go to cashOut. 

            do
            {
                // joinedInventory has the store's inventory. 
                // [0] Axe (5 gp, description);
                int i = 0 ;
                foreach (var item in joinedInventory)
                {
                    Console.WriteLine($"[{i}] {item.Name} ({item.Price} gp, {item.Description})");
                    i++;
                }

                // next: Get customer input. 
                input = 'x';
            } while(!(input.Equals('x')));
            // this will probably return an Order. 
        }
    }
}