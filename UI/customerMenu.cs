using BL;
using Models;
using System;

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