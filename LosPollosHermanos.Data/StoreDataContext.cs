using System.Data.Entity;
using LosPollosHermanos.Domain;

namespace LosPollosHermanos.Data
{
    public class StoreDataContext : DbContext
    {
        public StoreDataContext() : base("LosPollosHermanos")
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
