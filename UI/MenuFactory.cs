using DL;
using BL;
using System;

namespace UI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuString)
        {
            switch (menuString)
            {
                case "main":
                    return new MainMenu(new BLogic(new ExampleRepo()));
                case "store":
                    return new storeMenu(new BLogic(new ExampleRepo()));
                case "customer":
                    return new customerMenu(new BLogic(new ExampleRepo()));
                case "manager":
                    return new managerMenu(new BLogic(new ExampleRepo()));
                case "manageProducts":
                    return new manageProductsMenu(new BLogic(new ExampleRepo()));
                case "manageStores":
                    return new manageStoresMenu(new BLogic(new ExampleRepo()));
                default:
                    return null;
            }
        }
    }
}