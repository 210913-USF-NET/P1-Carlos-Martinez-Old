using System;
using Models;
using BL;
using Serilog;
using System.Collections.Generic;

namespace UI
{
    public class MainMenu : IMenu
    {
        private IBL _bl;
        public MainMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {
            bool exit = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nWelcome to the Emporium!");
                Console.WriteLine("Have you shopped with us before?");
                Console.WriteLine("   [0] Yes");
                Console.WriteLine("   [1] No");
                Console.WriteLine("   [2] Management");
                Console.WriteLine("   [3] Customer Profile");
                Console.WriteLine("   [x] Exit");
                Console.Write("Input: ");

                switch (Console.ReadLine())
                {
                    case "0":
                        // Request their name. Send them to the Store Menu. 
                        MenuFactory.GetMenu("store").Start();
                        break;

                    case "1":
                        // Send them to the new customer Method, then send them to the Store Menu. 
                        CreateCustomer();
                        break;

                    case "2":
                        // Send them to a management menu
                        Console.Write("Please enter your password: ");
                        if (Console.ReadLine() != "88")
                        {
                            Console.WriteLine("Validating... incorrect. Returning to main menu...");
                            break;
                        }
                        Console.WriteLine("Validating... thank you.");
                        MenuFactory.GetMenu("manager").Start();
                        break;
                    
                    case "3":
                        // Send them to the customer menu
                        MenuFactory.GetMenu("customer").Start();
                        break;
                    
                    case "x":
                        // Leave
                        exit = true;
                        break;
                }
            } while (!exit);
        }

        public void CreateCustomer()
        {
            // Do I want to move this elsewhere? Maybe just call _bl.CreateCustomer?
            // Except this information needs to ask things of the customer.
            // Need to gather information in the UI layer before sending it over. 
            // What do I want to gather?
            // Name, obviously. 
            // Age? No. Money? I could. Username? Eh. 
            Console.WriteLine("\nCreating new customer...");

            Console.Write("What is your name? ");
            string name = Console.ReadLine();

            List<Customer> allCustos = _bl.GetAllCustomers();
            Customer newCusto = new Customer(name);

            try 
            {
                newCusto.Name = name;
            }
            catch(InputInvalidException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine($"Thank you. Your ID to sign in is {allCustos.Count}.");
            _bl.AddCustomer(newCusto);

            // wait for the user to continue. 
            Console.Write("press enter to continue...");
            Console.ReadLine();
        }
    }
}