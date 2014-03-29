using System.Linq;
using LosPollosHermanos.Data;
using LosPollosHermanos.ServiceContracts;

namespace LosPollosHermanos.Web.Infrastructure
{
    public class ProductsService : IProductsService
    {
        private readonly StoreDataContext _dbContext = new StoreDataContext();

        public void UpdateProductStock(ProductAvailabilityRequest request)
        {
            var product = _dbContext.Products.First(x => x.Id == request.ProductId);

            product.IsAvailable = request.IsAvailable;
            _dbContext.Products.Attach(product);

            var entry = _dbContext.Entry(product);
            entry.Property(e => e.IsAvailable).IsModified = true;

            _dbContext.SaveChanges();
        }

        public AvailableProduct[] GetAvailableProducts()
        {
            return _dbContext.Products
                        .Where(x => x.IsAvailable)
                        .Select(x =>
                            new AvailableProduct
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Price = x.Price
                            })
                        .ToArray();
        }

        public Product[] GetAllProducts()
        {
            return _dbContext.Products
                        .Select(x =>
                            new Product
                            {
                                Id = x.Id,
                                Name = x.Name,
                                IsAvailable = x.IsAvailable
                            })
                        .ToArray();
        }
    }
}
