using BL;
using Models;
using System;

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
            Console.WriteLine("\nEnter your name: ");
            Customer custo = _bl.GetCustomer(Console.ReadLine());
            if (custo == null)
            {
                Console.WriteLine("Name not found. Returning to main menu.");
                return;
            }

            // Check default store. 

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