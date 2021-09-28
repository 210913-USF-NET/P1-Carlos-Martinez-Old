using BL;
using Models;
using System;
using System.Collections.Generic;

namespace UI
{
    public class customerMenu : IMenu
    {
        private IBL _bl;

        public customerMenu(IBL bl)
        {
            _bl = bl;
        }

        public void Start()
        {
            Console.WriteLine("\nEnter your unique ID: ");
            commonMethods _CMinstance = new commonMethods();
            int preinput = _CMinstance.convertString(Console.ReadLine(), 0);
            if (preinput == -1)
            {
                Console.WriteLine("ID not found. Returning to main menu.");
                return;
            }
            Customer custo = _bl.GetCustomer(preinput);
            if (custo == null)
            {
                Console.WriteLine("ID not found. Returning to main menu.");
                return;
            }

            // Check default store. 

            bool exit = false;
            string input;
            int parsedInput;
            bool parseSuccess;
            do
            {
                Console.WriteLine($"\nWelcome {custo.Name}!");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[0] View Past Orders");
                Console.WriteLine("[1] Change Default Store");
                Console.WriteLine("[2] Buy Credit");
                Console.WriteLine("[x] Exit");
                Console.Write("Input: ");

                switch (Console.ReadLine())
                {
                    case "0": 
                        // View Past Orders
                        Console.WriteLine("Not yet implemented.");
                        break; 

                    case "1": 
                        // Change Default Store
                        List<StoreFront> allRestos = _bl.GetAllStoreFronts();
                        if (allRestos.Count == 0)
                        {
                            Console.WriteLine("There are no stores.");
                            break;
                        }
                        getStore: 
                        for (int i = 0; i < allRestos.Count; i++) 
                        {
                            Console.WriteLine($"[{i}] {allRestos[i].Name}");
                        }
                        
                        Console.Write("Which store would you like to set? ");
                        input = Console.ReadLine();
                        parseSuccess = int.TryParse(input, out parsedInput);
                        if (parseSuccess && parsedInput >= 0 && parsedInput < allRestos.Count)
                        {
                            custo.StoreFrontID = parsedInput;
                            Console.WriteLine($"Storefront changed to {allRestos[parsedInput].Name}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input, please try again.");
                            goto getStore;
                        }


                        // update customer!
                        custo.hasDefaultStore = 1;
                        _bl.UpdateCustomer(custo);

                        break;

                    case "2": 
                        // Buy Credit
                        getPrice:
                        Console.Write("How much credit would you like to buy? ");
                        input = Console.ReadLine();
                        parseSuccess = int.TryParse(input, out parsedInput);
                        if (parseSuccess && parsedInput >= 0)
                        {
                            Console.WriteLine("\nAdding Credit.");                        
                            custo.Credit += parsedInput;
                        }
                        else
                        {
                            Console.WriteLine("Please type a non-negative integer number.");
                            goto getPrice;
                        }


                        // update customer!
                        _bl.UpdateCustomer(custo);

                        break;

                    case "x": 
                        exit = true;
                        break;
                }
            } while(!exit);
        }
    }
}