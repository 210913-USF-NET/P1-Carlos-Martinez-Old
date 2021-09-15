using System;
using Models;
using BL;
using DL;

namespace UI
{
    public class MainMenu : IMenu
    {
        public void Start()
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("\nWelcome to the Adventurer's Shop");
                Console.WriteLine("Have you shopped with us before?");
                Console.Write("0- Yes\n1- No\nx- Exit\nInput: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        // Request their name. Send them to the Store Menu. 
                        Console.WriteLine("Old customer, sign in");
                        break;

                    case "1":
                        // Send them to the new customer Method, then send them to the Store Menu. 
                        CreateCustomer();
                        break;
                    
                    case "x":
                        // Leave
                        Console.WriteLine("Exit!");
                        exit = true;
                        break;
                }
            } while (!exit);
        }

        public void CreateCustomer()
        {
            Console.WriteLine("Creating new customer...");
            Console.Write("What is your name? ");
            string name = Console.ReadLine();

            Console.WriteLine("Thank you.");

            Customer newCusto = new Customer(name);
        }
    }
}