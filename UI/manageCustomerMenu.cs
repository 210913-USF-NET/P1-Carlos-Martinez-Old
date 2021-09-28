using BL;
using System;
using Models;
using System.Collections.Generic;
using System.Linq;

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
                Console.WriteLine("[0] View Order History");
                Console.WriteLine("[1] Give Credit");
                Console.WriteLine("[2] Select Customer");
                Console.WriteLine("[x] Exit");
                Console.WriteLine("Input: ");
                
                switch(Console.ReadLine())
                {
                    case "0": 
                        // View Order History
                        if (activeCustomer == null)
                        {
                            Console.WriteLine("Please select a customer first.");
                            break;
                        }

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

                        List<Orders> custoOrders = _bl.orderList(activeCustomer.Id, choice);
                        List<StoreFront> allStores = _bl.GetAllStoreFronts();
                        List<Product> allProducts = _bl.GetAllProducts();

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
                        // Give Credit
                        if (activeCustomer == null)
                        {
                            Console.WriteLine("Please select a customer first.");
                            break;
                        }

                        getPrice:
                        Console.Write("How much credit would you like to give them? ");
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