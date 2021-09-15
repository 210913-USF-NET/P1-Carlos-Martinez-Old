using System;
using BL;
using DL;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            new MainMenu(new BLogic(new ExampleRepo())).Start();
        }
    }
}
