using BL;
using System;

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
            do
            {
                Console.WriteLine("\nWelcome to my shop!");
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