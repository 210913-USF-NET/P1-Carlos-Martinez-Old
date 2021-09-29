using BL;
using Models;
using System;
using System.Collections.Generic;

namespace UI
{
    public class manageStoresMenu : IMenu
    {
        private IBL _bl;

        public manageStoresMenu(IBL bl)
        {
            _bl = bl;
        }

        public void Start()
        {
            bool exit = false;
            StoreFront activeStore = null;
            do
            {
                Console.WriteLine("\nManaging stores...");
                if (activeStore == null) Console.WriteLine("No active store, please select a store first.");
                else Console.WriteLine($"Current active store: {activeStore.Name}"); // should display active store by name. 
                Console.WriteLine("   [0] Add Store");
                Console.WriteLine("   [1] Change Store");
                Console.WriteLine("   [2] See Inventory");
                Console.WriteLine("   [3] Add Inventory");
                Console.WriteLine("   [4] View Order History");
                Console.WriteLine("   [x] Return to manager menu");
                Console.Write("Input: ");

                switch (Console.ReadLine())
                {
                    case "0": 
                        // Add Store
                        Console.WriteLine("\nAdding a new store...");
                        Console.Write("New Store Name: ");
                        string name = Console.ReadLine();
                        activeStore = _bl.AddStoreFront(new StoreFront(name));
                        Console.WriteLine("Setting current active store to new store.");
                        break; 

                    case "1": 
                        // Change Store
                        Console.WriteLine("\nSearching for all stores...");

                        activeStore = selectStore();
                        if (activeStore is null)
                        {
                            Console.WriteLine("There are no stores.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Store changed to {activeStore.Name}");
                            break;
                        }

                    case "2": 
                        // See Inventory
                        if (activeStore is null)
                        {
                            Console.WriteLine("\nPlease select a store first.");
                            break;
                        }

                        SeeInventoryofStore(activeStore);
                        // wait for the user to continue. 
                        Console.Write("press enter to continue...");
                        Console.ReadLine();
                        break; 

                    case "3": 
                        // Add Inventory
                        if (activeStore is null)
                        {
                            Console.WriteLine("\nPlease select a store first.");
                            break;
                        }

                        string entry;
                        int quantity;
                        int parsedInput;
            
                        // List all products
                        // Ask which you want to add
                        // 
                        List<Product> allProducts = _bl.GetAllProducts();
                        seeProducts(allProducts);
                        Console.Write("\nWhich product would you like to add? ");

                        entry = Console.ReadLine();
                        parsedInput = _bl.convertString(entry, 0, allProducts.Count+1);

                        if (parsedInput == -1)
                        {
                            Console.WriteLine("Please enter a valid number. Returning to store manager menu.");
                            break;
                        }
                        else
                        {
                            Console.Write("How many would you like to add? ");
                            entry = Console.ReadLine();
                            quantity = _bl.convertString(entry, 0);
                            if (quantity == -1)
                            {
                                Console.WriteLine("Please enter a valid number. Returning to store manager menu.");
                                break;
                            }
                            else
                            {
                                // THIS does not work. 
                                Inventory addedProduct = new Inventory(parsedInput, activeStore.Id, quantity);
                                _bl.AddInventory(addedProduct);
                            }
                        }
                        
                        break; 

                    case "4":
                        // View Order History
                        if (activeStore == null)
                        {
                            Console.WriteLine("\nPlease select a store first.");
                            break;
                        }
                        string choice = "";

                        chooseOrder:
                        Console.Write("\nWould you like the orders by [D]ate or [T]otal? ");
                        switch(Char.ToUpper(Console.ReadLine()[0]))
                        {
                            case 'D':
                                // they want it by DATE
                                choice = choice + "D";
                                break; 
                            case 'T': 
                                // they want it by TOTAL
                                choice = choice + "T";
                                break; 
                            default: 
                                // they fucked up!
                                Console.WriteLine("Please input a proper response.");
                                goto chooseOrder;
                        }

                        AscOrDes: 
                        Console.Write("Would you like the orders in [A]scending or [D]escending order? ");
                        switch(Char.ToUpper(Console.ReadLine()[0]))
                        {
                            case 'A':
                                // they want it by ASCENDING
                                choice = choice + "A";
                                break; 
                            case 'D': 
                                // they want it by DESCENDING
                                choice = choice + "D";
                                break; 
                            default: 
                                // they fucked up!
                                Console.WriteLine("Please input a proper response.");
                                goto AscOrDes;
                        }
                        Console.WriteLine();
                        
                        List<Orders> storeOrders = _bl.storeOrders(activeStore.Id, choice);
                        List<Customer> allCustos = _bl.GetAllCustomers();
                        List<Product> allProductsforThisPlace = _bl.GetAllProducts();

                        if(storeOrders.Count == 0)
                        {
                            Console.WriteLine("This store has no history.");
                            break;
                        }

                        // List of Line Items by Order
                        List<List<LineItem>> storeLineItems = new List<List<LineItem>>();
                        foreach (Orders item in storeOrders)
                        {
                            List<LineItem> LineItemsbyOrder = _bl.GetLineItembyOrderID(item.Id);
                            storeLineItems.Add(LineItemsbyOrder);
                        }

                        for (int i = 0; i < storeOrders.Count; i++)
                        {
                            Console.WriteLine($"On {storeOrders[i].Date.ToString("g")}, {allCustos[storeOrders[i].CustomerId].Name} paid {storeOrders[i].Total} gold coins for:");
                            foreach (LineItem lineitem in storeLineItems[i])
                            {
                                Console.WriteLine($" * {allProductsforThisPlace[lineitem.ProductId].Name} x{lineitem.Quantity}");
                            }
                            // can insert a readline command to wait for user input. 
                            // can have it show up every five orders, so the user isn't flooded. 
                            // can have it read the input and, if x, doesn't stop anymore. 
                        }
                        
                        // wait for the user to continue. 
                        Console.Write("press enter to continue...");
                        Console.ReadLine();

                        break;
                        
                    case "x": 
                        exit = true;
                        break;
                }
            } while(!exit);
        }
        public StoreFront selectStore()
        {
            List<StoreFront> allRestos = _bl.GetAllStoreFronts();
            if (allRestos.Count == 0)
            {
                return null;
            }

            selectStore:
            for (int i = 0; i < allRestos.Count; i++) 
            {
                Console.WriteLine($"   [{i}] {allRestos[i].Name}");
            }
            Console.Write("Which store would you like to switch to? ");
            string input = Console.ReadLine();
            int parsedInput;

            bool parseSuccess = int.TryParse(input, out parsedInput);
            if (parseSuccess && parsedInput >= 0 && parsedInput < allRestos.Count)
            {
                return allRestos[parsedInput];
            }
            else
            {
                Console.WriteLine("Invalid input.");
                goto selectStore;
            }
        }

        public void seeProducts(List<Product> allProducts)
        {
            // Goes through each item in the list provided and displays information about them. 
            string currentProduct;
            for (int i = 0; i < allProducts.Count; i++)
            {
                currentProduct = $"   [{i}] {allProducts[i].Name}";
                currentProduct = currentProduct + $" ({allProducts[i].Price})";
                currentProduct = currentProduct + $": {allProducts[i].Description}";
                Console.WriteLine(currentProduct);
            }
        }

        private void SeeInventoryofStore(StoreFront activeStore) {
            // print the inventory of a store
            List<Inventory> storeInventory = _bl.GetInventory(activeStore.Id);

            if (storeInventory.Count == 0)
            {
                Console.WriteLine("The Store is empty!");
                return;
            }

            for (int i = 0; i < storeInventory.Count; i++)
            {
                Console.WriteLine($"   [{i}] {storeInventory[i]}");
            }
        }
        
    }
}