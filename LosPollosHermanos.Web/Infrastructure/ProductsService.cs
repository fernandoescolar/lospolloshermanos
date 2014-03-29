using System.Linq;
using LosPollosHermanos.Data;
using LosPollosHermanos.Infrastructure;
using LosPollosHermanos.ServiceContracts;
using LosPollosHermanos.Web.Hubs;
using Microsoft.AspNet.SignalR;

namespace LosPollosHermanos.Web.Infrastructure
{
    public class ProductsService : IProductsService
    {
        private readonly StoreDataContext _dbContext = new StoreDataContext();

        public ProductsService()
        {
            var helper = ServiceBusTopicHelper.Setup(SubscriptionInitializer.Initialize());
            helper.Subscribe<OrderRequest>((order) =>
                {
                    GlobalHost.ConnectionManager.GetHubContext<OrderStatusHub>().Clients.All().statusUpdated("ordered");
                }
                   , "(IsOrdered = true) AND (Procesing = false) AND (IsDelivered = false)",
                   "StatusOrderedOrders"
               );
            helper.Subscribe<OrderRequest>((order) =>
            {
                GlobalHost.ConnectionManager.GetHubContext<OrderStatusHub>().Clients.All().statusUpdated("processing");
            }
                   , "(IsOrdered = true) AND (Procesing = true) AND (IsDelivered = false)",
                   "StatusProcesingOrders"
               );
            helper.Subscribe<OrderRequest>((order) =>
            {
                GlobalHost.ConnectionManager.GetHubContext<OrderStatusHub>().Clients.All().statusUpdated("delivered");
            }
                   , "(IsOrdered = true) AND (Procesing = true) AND (IsDelivered = true)",
                   "StatusDeliveredOrders"
               );
        }

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
