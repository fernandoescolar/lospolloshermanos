using System;
using System.Collections.Generic;
using System.Linq;
using LosPollosHermanos.Data;
using LosPollosHermanos.Domain;
using LosPollosHermanos.ServiceContracts;

namespace LosPollosHermanos.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly StoreDataContext _dbContext = new StoreDataContext();

        public void SendOrder(OrderRequest request)
        {
            var order = new Order
                        {
                            ClientId = request.Client,
                            OrderedTimeStamp = DateTime.UtcNow,
                            OderLines = request.Lines.Select(x => new OrderLine
                                                                  {
                                                                      ProductId = x.ProductId,
                                                                      Quantity = x.Quantity
                                                                  }).ToList()
                        };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            Console.WriteLine("Order {0} just taken with {1} product(s)",
                        order.Id,
                        order.OderLines.Count);
        }

        public void UpdateOrder(UpdateOrderRequest request)
        {
            if (request.Status == OrderStatus.Delivered)
            {
                var order = _dbContext.Orders.First(x => x.Id == request.OrderId);
                order.DeliveredTimeStamp = DateTime.UtcNow;
                _dbContext.Orders.Attach(order);

                var entry = _dbContext.Entry(order);
                entry.Property(e => e.DeliveredTimeStamp).IsModified = true;

                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<OrderRequest> GetPendingOrders()
        {
            var orders = _dbContext.Orders.Include("OrderLines").Where(x => !x.IsReceivedByStore).ToList();
            var result = orders.Select(x => new OrderRequest
                                            {
                                                Client = x.ClientId,
                                                OrderId = x.Id,
                                                Lines = x.OderLines.Select(o => new OrderRequestLine
                                                                                {
                                                                                    ProductId = o.ProductId,
                                                                                    Quantity = o.Quantity
                                                                                }).ToArray()
                                            });

            foreach (var order in orders)
            {
                order.IsReceivedByStore = true;
                _dbContext.Orders.Attach(order);

                var entry = _dbContext.Entry(order);
                entry.Property(e => e.IsReceivedByStore).IsModified = true;
            }

            _dbContext.SaveChanges();

            return result;
        }
    }
}
