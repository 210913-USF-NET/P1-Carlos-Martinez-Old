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

            int preinput = _bl.convertString(Console.ReadLine(), 0);
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
                Console.WriteLine("   [0] View Past Orders");
                Console.WriteLine("   [1] Change Default Store");
                Console.WriteLine("   [2] Buy Credit");
                Console.WriteLine("   [x] Return to main menu");
                Console.Write("Input: ");

                switch (Console.ReadLine())
                {
                    case "0": 
                        // View Order History

                        string choice = "";

                        chooseOrder:
                        Console.Write("Would you like the orders by [D]ate or [T]otal? ");
                        switch(Char.ToUpper(Console.ReadLine()[0]))
                        {
                            case 'D':
                                // they want it by DATE
                                choice = choice + "D";
                                break; 
                            case 'T': 
                                // they want it by TOTAL
                                choice = choice + "T";
                                break; 
                            default: 
                                // they fucked up!
                                Console.WriteLine("Please input a proper response.");
                                goto chooseOrder;
                        }

                        AscOrDes: 
                        Console.Write("Would you like the orders in [A]scending or [D]escending order? ");
                        switch(Char.ToUpper(Console.ReadLine()[0]))
                        {
                            case 'A':
                                // they want it by ASCENDING
                                choice = choice + "A";
                                break; 
                            case 'D': 
                                // they want it by DESCENDING
                                choice = choice + "D";
                                break; 
                            default: 
                                // they fucked up!
                                Console.WriteLine("Please input a proper response.");
                                goto AscOrDes;
                        }

                        List<Orders> custoOrders = _bl.orderList(custo.Id, choice);
                        List<StoreFront> allStores = _bl.GetAllStoreFronts();
                        List<Product> allProducts = _bl.GetAllProducts();

                        if(custoOrders.Count == 0)
                        {
                            Console.WriteLine("You have no history.");
                        }

                        List<List<LineItem>> custoLineItems = new List<List<LineItem>>();
                        foreach (Orders item in custoOrders)
                        {
                            List<LineItem> LineItemsbyOrder = _bl.GetLineItembyOrderID(item.Id);
                            custoLineItems.Add(LineItemsbyOrder);
                        }

                        for (int i = 0; i < custoOrders.Count; i++)
                        {
                            Console.WriteLine($"On {custoOrders[i].Date.ToString("g")}, at {allStores[custoOrders[i].StoreFrontId].Name}, they paid {custoOrders[i].Total} gold coins for:");
                            foreach (LineItem lineitem in custoLineItems[i])
                            {
                                Console.WriteLine($" * {allProducts[lineitem.ProductId].Name} x{lineitem.Quantity}");
                            }
                            // can insert a readline command to wait for user input. 
                            // can have it show up every five orders, so the user isn't flooded. 
                            // can have it read the input and, if x, doesn't stop anymore. 
                        }
                        
                        // wait for the user to continue. 
                        Console.Write("press enter to continue...");
                        Console.ReadLine();

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
                            Console.WriteLine($"   [{i}] {allRestos[i].Name}");
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
                            Console.WriteLine("\nAdding credit...");                        
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