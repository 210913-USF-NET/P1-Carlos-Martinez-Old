using BL;
using System;
using Models;
using System.Collections.Generic;

namespace UI
{
    public class manageCustomerMenu : IMenu
    {
        private IBL _bl;

        public manageCustomerMenu(IBL bl)
        {
            _bl = bl;
        }

        public void Start()
        {
            bool exit = false;
            string input;
            int parsedInput;
            bool parseSuccess;
            Customer activeCustomer = null;
            do
            {
                Console.WriteLine("\nManaging customers...");
                if (activeCustomer == null) Console.WriteLine("No active customer, please select a customer first.");
                else Console.WriteLine($"Current active customer: {activeCustomer.Name}"); // should display active customer by name. 
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("0- View Order History");
                Console.WriteLine("1- Give Credit");
                Console.WriteLine("2- Select Customer");
                Console.WriteLine("x- Exit");
                Console.WriteLine("Input: ");
                
                switch(Console.ReadLine())
                {
                    case "0": 
                        // NYI, View Order History
                        if (activeCustomer == null)
                        {
                            Console.WriteLine("Please select a customer first.");
                            break;
                        }
                        break;
                        
                    case "1": 
                        // Give Credit
                        if (activeCustomer == null)
                        {
                            Console.WriteLine("Please select a customer first.");
                            break;
                        }

                        getPrice:
                        Console.Write("How much credit would you like to give them?");
                        input = Console.ReadLine();
                        parseSuccess = int.TryParse(input, out parsedInput);
                        if (parseSuccess && parsedInput >= 0)
                        {
                            Console.WriteLine("\nAdding Credit.");                        
                            activeCustomer.Credit += parsedInput;
                        }
                        else
                        {
                            Console.WriteLine("Please type a non-negative integer number.");
                            goto getPrice;
                        }


                        // update customer!
                        _bl.UpdateCustomer(activeCustomer);

                        break;
                        
                    case "2": 
                        // List Customer
                        Console.WriteLine("Searching for all customer...");
                        List<Customer> allCustomer = _bl.GetAllCustomers();
                        if (allCustomer.Count == 0)
                        {
                            Console.WriteLine("There are no customers.");
                            break;
                        }
                        for (int i = 0; i < allCustomer.Count; i++) 
                        {
                            Console.WriteLine($"[{i}] {allCustomer[i].Name}");
                        }
                        Console.WriteLine("Which customer would you like to look at?");
                        input = Console.ReadLine();

                        parseSuccess = int.TryParse(input, out parsedInput);
                        if (parseSuccess && parsedInput >= 0 && parsedInput < allCustomer.Count)
                        {
                            activeCustomer = allCustomer[parsedInput];
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Returning to customer manager menu.");
                            break;
                        }
                        break; 
                        
                    case "x": 
                        exit = true;
                        break;
                    
                }
            } while(!exit);
        }
    }
}