using System.Data.Entity.Migrations;
using LosPollosHermanos.Domain;

namespace LosPollosHermanos.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<StoreDataContext>
    {
        public Configuration()
        {
            SetSqlGenerator("System.Data.SqlClient", new MigrationsGenerator());
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StoreDataContext context)
        {
            context.Products.AddOrUpdate(
                p => p.Name,
                new Product { Id = 1, Name = "Crispy Strips", IsAvailable = true, Price = 9.45m },
                new Product { Id = 2, Name = "Menú Bucket", IsAvailable = true, Price = 17.90m },
                new Product { Id = 3, Name = "Hot Wings", IsAvailable = true, Price = 8.10m },
                new Product { Id = 4, Name = "Brazer Wrap", IsAvailable = true, Price = 7.65m },
                new Product { Id = 5, Name = "BoxMaster", IsAvailable = true, Price = 6.85m },
                new Product { Id = 6, Name = "Los Pollos Sticks", IsAvailable = true, Price = 2.50m },
                new Product { Id = 7, Name = "Pep’Hits", IsAvailable = true, Price = 9.75m },
                new Product { Id = 8, Name = "Blue Meth", IsAvailable = true, Price = 40.00m }
             );
        }
    }
}
