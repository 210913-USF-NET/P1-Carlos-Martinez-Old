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
                Console.WriteLine("[0] Add Store");
                Console.WriteLine("[1] Change Store");
                Console.WriteLine("[2] See Inventory");
                Console.WriteLine("[3] Add Inventory");
                Console.WriteLine("[x] Exit");
                Console.Write("Input: ");

                switch (Console.ReadLine())
                {
                    case "0": 
                        // Add Store
                        Console.WriteLine("Adding a new store. What is the store's name?");
                        string name = Console.ReadLine();
                        activeStore = _bl.AddStoreFront(new StoreFront(name));
                        Console.WriteLine("Setting current active store to new store.");
                        break; 

                    case "1": 
                        // Change Store
                        Console.WriteLine("Searching for all stores...");

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
                            Console.WriteLine("Please select a store first.");
                            break;
                        }

                        SeeInventoryofStore(activeStore);
                        break; 

                    case "3": 
                        // Add Inventory
                        if (activeStore is null)
                        {
                            Console.WriteLine("Please select a store first.");
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
                        Console.Write("Which product would you like to add? ");

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

                        /*
                        parseSuccess = int.TryParse(input, out parsedInput);
                        if (parseSuccess && parsedInput >= 0 && parsedInput < allProducts.Count)
                        {
                            // add item to inventory for the selected store. 
                            Inventory addedProduct = new Inventory(allProducts[parsedInput].Id, activeStore.Id, quantity);
                            activeStore.Inventories.Add(allProducts[parsedInput]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Returning to product manager menu.");
                            break;
                        } */
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
                Console.WriteLine($"[{i}] {allRestos[i].Name}");
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
                currentProduct = $"[{i}] {allProducts[i].Name}";
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
                Console.WriteLine($"[{i}] {storeInventory[i]}");
            }
        }
        
    }
}