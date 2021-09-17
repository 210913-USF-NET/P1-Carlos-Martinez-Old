using DL;
using BL;
using System;

namespace UI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuString)
        {
            switch (menuString.ToLower())
            {
                case "main":
                    return new MainMenu(new BLogic(new ExampleRepo()));
                case "store":
                    return new StoreMenu(new BLogic(new ExampleRepo()));
                case "manager":
                    return new ManagerMenu(new BLogic(new ExampleRepo()));
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