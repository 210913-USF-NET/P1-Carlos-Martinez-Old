using DL;
using BL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace UI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuString)
        {
            // Setting up DB connections. Second line is red atm. 
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<LinguzRevatureStoreContext> options = new DbContextOptionsBuilder<LinguzRevatureStoreContext>().UseSqlServer(connectionString).Options;
            LinguzRevatureStoreContext context = new LinguzRevatureStoreContext(options);

            switch (menuString)
            {
                case "main":
                    return new MainMenu(new BLogic(new DBRepo(context)));
                case "store":
                    return new storeMenu(new BLogic(new DBRepo(context)));
                case "customer":
                    return new customerMenu(new BLogic(new DBRepo(context)));
                case "manager":
                    return new managerMenu(new BLogic(new DBRepo(context)));
                case "manageProducts":
                    return new manageProductsMenu(new BLogic(new DBRepo(context)));
                case "manageStores":
                    return new manageStoresMenu(new BLogic(new DBRepo(context)));
                case "manageCustomers":
                    return new manageCustomerMenu(new BLogic(new DBRepo(context)));
                default:
                    return null;
            }
        }
    }
}