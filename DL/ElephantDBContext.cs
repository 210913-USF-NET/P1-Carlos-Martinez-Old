using Microsoft.EntityFrameworkCore;
using Models;

namespace DL
{
    public class ElephantDBContext : DbContext
    {
        public ElephantDBContext() : base() { }
        public ElephantDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<StoreFront> StoreFronts { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<LineItem> LineItem { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}