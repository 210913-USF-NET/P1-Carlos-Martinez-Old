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
                Console.WriteLine("0- Add Store");
                Console.WriteLine("1- Change Store");
                Console.WriteLine("2- See Inventory");
                Console.WriteLine("3- Add Inventory");
                Console.WriteLine("x- Exit");
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
                        break; 
                    case "3": 
                        // Add Inventory
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
    }
}