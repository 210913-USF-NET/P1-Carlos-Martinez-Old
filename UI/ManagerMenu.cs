using System;
using BL;

namespace UI
{
    public class managerMenu : IMenu
    {
        private IBL _bl;

        public managerMenu(IBL bl)
        {
            _bl = bl;
        }

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            bool exit = false;
            do
            {
                Console.WriteLine("\nWelcome to the manager menu.");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("   [0] Manage Products");
                Console.WriteLine("   [1] Manage Stores");
                Console.WriteLine("   [2] Manage Customers");
                Console.WriteLine("   [x] Return to main menu");
                Console.Write("Input: ");

                switch (Console.ReadLine())
                {
                    case "0":
                        // Add or remove products
                        MenuFactory.GetMenu("manageProducts").Start();
                        break;
                    case "1":
                        // Add or remove stores. 
                        // Adjust inventories within stores. 
                        MenuFactory.GetMenu("manageStores").Start();
                        break;
                    case "2":
                        // Add or remove stores. 
                        // Adjust inventories within stores. 
                        MenuFactory.GetMenu("manageCustomers").Start();
                        break;
                    case "x": 
                        // Leave
                        exit = true;
                        break;
                }
            } while(!exit);
        }
    }
}