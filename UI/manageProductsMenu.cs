using BL;
using System;
using Models;
using System.Collections.Generic;

namespace UI
{
    public class manageProductsMenu : IMenu
    {
        private IBL _bl;

        public manageProductsMenu(IBL bl)
        {
            _bl = bl;
        }

        public void Start()
        {
            bool exit = false;
            string input;
            int parsedInput;
            bool parseSuccess;
            Product activeProduct = null;
            do
            {
                Console.WriteLine("\nManaging product...");
                if (activeProduct == null) Console.WriteLine("No active product, please select a product first.");
                else Console.WriteLine($"Current active product: {activeProduct.Name}"); // should display active product by name. 
                Console.WriteLine("0- Add Product");
                Console.WriteLine("1- Change Active Product");
                Console.WriteLine("2- Remove Product");
                Console.WriteLine("x- Exit");
                Console.Write("Input: ");

                switch (Console.ReadLine())
                {
                    case "0": 
                        // Add Product
                        Console.WriteLine("Adding a new product. What is the product's name?");
                        string name = Console.ReadLine();
                        
                        getPrice:
                        Console.Write("Price: ");
                        input = Console.ReadLine();
                        parseSuccess = int.TryParse(input, out parsedInput);
                        if (parseSuccess && parsedInput > 0)
                        {
                            Console.WriteLine("\nDescribe the product.");
                            string description = Console.ReadLine();
                        
                            activeProduct = _bl.AddProduct(new Product(name, parsedInput, description));
                        }
                        else
                        {
                            Console.WriteLine("Please type an integer number.");
                            goto getPrice;
                        }

                        break; 

                    case "1": 
                        // List Products
                        Console.WriteLine("Searching for all products...");
                        List<Product> allProducts = _bl.GetAllProducts();
                        if (allProducts.Count == 0)
                        {
                            Console.WriteLine("There are no products.");
                            break;
                        }
                        for (int i = 0; i < allProducts.Count; i++) 
                        {
                            Console.WriteLine($"[{i}] {allProducts[i].Name}");
                        }
                        Console.WriteLine("Which product would you like to look at?");
                        input = Console.ReadLine();

                        parseSuccess = int.TryParse(input, out parsedInput);
                        if (parseSuccess && parsedInput >= 0 && parsedInput < allProducts.Count)
                        {
                            activeProduct = allProducts[parsedInput];
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Returning to product manager menu.");
                            break;
                        }
                        break; 

                    case "2": 
                        // Remove Product
                        Console.WriteLine("Not yet implemented.");
                        break; 

                    case "x": 
                        exit = true;
                        break;
                }
            } while(!exit);
        }
    }
}