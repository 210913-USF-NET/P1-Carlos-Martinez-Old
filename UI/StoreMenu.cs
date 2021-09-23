using BL;
using Models;
using System;
using System.Collections.Generic;  

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
            
            if(custo.StoreFrontID != -1)
            {
                // They have a default store. 
                activeStore = allRestos[custo.StoreFrontID];
            }
            else
            {
                manageStoresMenu _MSMinstance = new manageStoresMenu(_bl);
                activeStore = _MSMinstance.selectStore();
                Console.WriteLine($"Setting default store to {activeStore.Name}");
                custo.StoreFrontID = activeStore.Id;
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
                        break; 
                    case "1": 
                        // Change Store
                        break; 
                    case "2": 
                        // Check Order
                          // Checkout, Remove Product from Order
                          // Should I make a switch inside the switch or send it over to a new menu?
                        break; 
                    case "3": 
                        exit = true;
                        break;
                }
            } while(!exit);
        }
    }
}