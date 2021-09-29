using BL;
using Models;
using System;
using System.Collections.Generic;  
using System.Linq;

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
            int input = _bl.convertString(Console.ReadLine(), 0);
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
            
            if(custo.hasDefaultStore.Equals(1))
            {
                // They have a default store. 
                activeStore = allRestos[custo.StoreFrontID];
            }
            else
            {
                Console.WriteLine("Where would you like to shop?");
                manageStoresMenu _MSMinstance = new manageStoresMenu(_bl);
                activeStore = _MSMinstance.selectStore();
                Console.WriteLine($"Setting default store to {activeStore.Name}.");
                custo.StoreFrontID = activeStore.Id;
                custo.hasDefaultStore = 1;
                _bl.UpdateCustomer(custo);
            }

            bool exit = false;
            do
            {
                Console.WriteLine($"\nWelcome to {activeStore.Name}!");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("   [0] Add Product to Shopping Cart");
                Console.WriteLine("   [1] Change Store");
                Console.WriteLine("   [x] Return to main menu");
                Console.Write("Input: ");

                switch (Console.ReadLine())
                {
                    case "0": 
                        // Add Product to Order
                        addProductToOrder(activeStore, custo);
                        break; 

                    case "1": 
                        // Change Store
                        Console.WriteLine("\nSearching for all stores...");

                        manageStoresMenu _instance = new manageStoresMenu(_bl);
                        activeStore = _instance.selectStore();
                        if (activeStore is null)
                        {
                            Console.WriteLine("There are no stores.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Store changed to {activeStore.Name}");
                            custo.StoreFrontID = activeStore.Id;
                            _bl.UpdateCustomer(custo);
                            break;
                        }

                    case "x": 
                        exit = true;
                        break;
                }
            } while(!exit);
        }

        private void addProductToOrder(StoreFront store, Customer custo)
        {
            if (custo.Credit == 0)
            {
                Console.WriteLine("\nYou have no credit. Please add some before shopping.");
                return;
            }
            else if (custo.Credit < 0)
            {
                Console.WriteLine($"\nYou have a past due balance of {custo.Credit} gold coins.");
                Console.WriteLine("Please return to a positive balance before browsing.");
                return;
            }

            Orders currentOrder = new Orders(custo.Id, store.Id);
            currentOrder.Id = _bl.GetAllOrders().Count;

            int runningTotal = 0;

            // Need to join storeInventory and Product
            List<Inventory> storeInventory = _bl.GetInventory(store.Id);
            List<Product> allProducts = _bl.GetAllProducts();
            List<joinedInventory> joinedInventory = new List<joinedInventory>();
            List<joinedInventory> LineItemInfo = new List<joinedInventory>();


            // var tempInventory = from m1 in storeInventory
            //     join m2 in allProducts on m1.ProductId equals m2.Id
            //     select new {m1.Id, m2.Name, m1.Quantity, m2.Price, m2.Description};

            // also create a lineitem every time the following loop is active. 
            string input;

            // easier to parse through it if it's a list...
            // int trueQuantity;
            // int j = 0;
            for (int j = 0; j<storeInventory.Count; j++)
            {
                if (storeInventory[j].StoreFrontId == store.Id)
                {
                    joinedInventory.Add(new joinedInventory(
                        storeInventory[j].Id, 
                        allProducts[storeInventory[j].ProductId].Name, 
                        storeInventory[j].Quantity, 
                        allProducts[storeInventory[j].ProductId].Price, 
                        allProducts[storeInventory[j].ProductId].Description
                ));
                }
            }
            // foreach (var item in tempInventory)
            // {
            //     trueQuantity = storeInventory[j].Quantity;
            //     joinedInventory.Add(new joinedInventory(item.Id, item.Name, trueQuantity, item.Price, item.Description));
            //     j++;
            // }
            List<LineItem> LineItemHolder = new List<LineItem>();

            do
            {
                // joinedInventory has the store's inventory. 
                // [0] Axe (5 gp, description);
                int i = 0;
                int quantity = 0;
                Console.WriteLine("");
                foreach (joinedInventory item in joinedInventory)
                {
                    // If each item in the store's inventory has any quantity, 
                    // print said item. Otherwise, skip it. 
                    // Assumption: Every store will have every item. 
                    quantity = item.Quantity;
                    if (LineItemInfo.Count != 0)
                    {
                        foreach (joinedInventory lineitem in LineItemInfo)
                        {
                            if (lineitem.Id == item.Id)
                            {
                                quantity -= lineitem.Quantity;
                            }
                        }
                    }

                    if (quantity == 0) Console.WriteLine($"   [{i}] {item.Name}: Out of Stock");
                    else Console.WriteLine($"   [{i}] {item.Name} ({item.Price} gp, {quantity} available, {item.Description})");
                    i++;
                }
                Console.WriteLine("   [x] Exit or Checkout");

                // next: Get customer input. 
                command:
                Console.Write("Enter your command: ");
                input = Console.ReadLine();
                if (input == "x")
                {
                    break;
                }

                int parsedInt = _bl.convertString(input, 0, joinedInventory.Count-1);

                if (parsedInt == -1)
                {
                    Console.WriteLine("Please enter a valid input.");
                    goto command;
                } else if (joinedInventory[parsedInt].Quantity == 0)
                {
                    Console.WriteLine("We do not have that product currently in store.");
                    goto command;
                }
                // parsedInt should contain the index of the item the customer wants now. 

                quantity:
                Console.Write($"How many {joinedInventory[parsedInt].Name}(s) do you want? \nThere are {joinedInventory[parsedInt].Quantity} available. ");
                input = Console.ReadLine();
                int amount = _bl.convertString(input, 1);

                if (amount == -1)
                {
                    Console.WriteLine("Please enter a valid input.");
                    goto quantity;
                } else if (amount > joinedInventory[parsedInt].Quantity)
                {
                    Console.WriteLine($"You are limited to buying {joinedInventory[parsedInt].Quantity} currently.");
                    amount = joinedInventory[parsedInt].Quantity;
                }
                LineItem currentItem = new LineItem(currentOrder.Id, parsedInt, amount);
                LineItemHolder.Add(currentItem);
                runningTotal = runningTotal + (amount * joinedInventory[parsedInt].Price);
                joinedInventory currentInv = new joinedInventory(joinedInventory[parsedInt]);
                LineItemInfo.Add(currentInv);
                LineItemInfo.Last().Quantity = amount;
            } while(!(input.Equals('x')));


            // They have exited at this point with their full order. 
            currentOrder.Total = runningTotal;
            bool cancel = false;

            cancel:
            if (LineItemInfo.Count == 0 || cancel)
            {
                Console.WriteLine("Cancelling order...");
                return;
            }

            char confirmation;
            while (true)
            {
                // print out their current order.
                for (int i = 0; i < LineItemInfo.Count; i++)
                {
                    Console.WriteLine($"[{i}] {LineItemInfo[i].Name} x{LineItemInfo[i].Quantity} ({LineItemInfo[i].Price * LineItemInfo[i].Quantity} gp, {LineItemInfo[i].Description})");
                }
                Console.WriteLine($"Total: {currentOrder.Total} gold pieces");
                Console.WriteLine("\n[x] Cancel Order");
                Console.WriteLine("[c] Check Out Order");
                input = Console.ReadLine();
                switch (input)
                {
                    case "x":
                        Console.WriteLine("Type 'Y' to confirm cancelling your order.");
                        confirmation = Char.ToUpper(Console.ReadLine()[0]);
                        if (confirmation.Equals('Y')) 
                        {
                            cancel = true;
                            goto cancel;
                        }
                        break;
                    
                    case "c":
                        Console.WriteLine("Type 'Y' to check out.");
                        confirmation = Char.ToUpper(Console.ReadLine()[0]);
                        if (confirmation.Equals('Y'))
                        {
                            goto checkOut;
                        }
                        break;
                    
                    default:
                        Console.WriteLine("Invalid entry.");
                        break;
                }
            }

            checkOut: 

            custo.Credit = custo.Credit - currentOrder.Total;
            if (custo.Credit < 0)
            {
                Console.WriteLine($"You owe {Math.Abs(custo.Credit)} gold coins. Please pay your balance due at your earliest convenience.");
            } else {
                Console.WriteLine($"You have {custo.Credit} gold coins left.");
            }

            foreach (joinedInventory item in LineItemInfo)
            {
                // This needs to be fixed. 
                foreach (Inventory itemToUpdate in storeInventory)
                {
                    // itemToUpdate.Id is the INVENTORY ID. 
                    if (itemToUpdate.Id == item.Id)
                    {
                        itemToUpdate.Quantity -= item.Quantity;
                    }
                }
            }
            
            _bl.AddOrder(currentOrder);
            _bl.AddLineItem(LineItemHolder);
            _bl.UpdateInventory(storeInventory);
            _bl.UpdateCustomer(custo);
            // this will probably return an Order. 
        }
    }
}