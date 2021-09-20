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
            bool exit = false;
            do
            {
                Console.WriteLine("Welcome to the manager menu.");
                Console.Write("Please enter your password: ");
                if (Console.ReadLine() != "88")
                {
                    Console.WriteLine("Validating... incorrect. Returning to main menu...");
                    return;
                }

                Console.WriteLine("Validating... thank you.");

                Console.WriteLine("What would you like to do?");
                Console.WriteLine("0- Manage Products");
                Console.WriteLine("1- Manage Stores");
                Console.WriteLine("x- Exit");
                Console.WriteLine("Input: ");

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
                    case "x": 
                        // Leave
                        exit = true;
                        break;
                }
            } while(!exit);
        }
    }
}