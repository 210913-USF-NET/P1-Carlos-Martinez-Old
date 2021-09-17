using BL;
using Models;
using System;

namespace UI
{
    public class StoreMenu : IMenu
    {
        private IBL _bl;

        public StoreMenu(IBL bl)
        {
            _bl = bl;
        }

        public void Start()
        {
            Console.WriteLine("\nEnter your name: ");
            Customer custo = _bl.GetCustomer(Console.ReadLine());
            if (custo == null)
            {
                Console.WriteLine("Name not found. Returning to main menu.");
                return;
            }

            bool exit = false;
            do
            {
                Console.WriteLine("Entered Store. Press anything to leave.");
                Console.ReadLine();
                exit = true;
            } while(!exit);
        }
    }
}