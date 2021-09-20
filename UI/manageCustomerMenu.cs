using BL;
using System;

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
            do
            {
                Console.WriteLine("Entered Store. Press anything to leave.");
                Console.ReadLine();
                exit = true;
            } while(!exit);
        }
    }
}