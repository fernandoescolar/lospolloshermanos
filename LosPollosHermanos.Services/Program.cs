using System;
using System.ServiceModel;
using LosPollosHermanos.Infrastructure;
using LosPollosHermanos.ServiceContracts;

namespace LosPollosHermanos.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ordersServiceHost = new ServiceHost(typeof (OrdersService));
            //var productsServiceHost = new ServiceHost(typeof(ProductsService));
            var helper = ServiceBusTopicHelper.Setup(SubscriptionInitializer.Initialize());

            try
            {
                //ordersServiceHost.Open();
                //productsServiceHost.Open();
                helper.Subscribe<OrderRequest>((order) =>
                                               {
                                                   var service = new OrdersService();
                                                   service.SendOrder(order);
                                               }
                    , "(IsOrdered = false) AND (IsDelivered = false)",
                    "Orders"
                );

                helper.Subscribe<OrderRequest>((order) =>
                                                {
                                                    var service = new OrdersService();
                                                    service.UpdateOrder(new UpdateOrderRequest
                                                                        {
                                                                            OrderId = order.OrderId,
                                                                            Status = OrderStatus.Delivered
                                                                        });
                                                }
                   , "(IsOrdered = true) AND (IsDelivered = true)",
                   "Orders"
               );

                Console.WriteLine("Press intro to exit");
                Console.ReadLine();

                //ordersServiceHost.Close();
                //productsServiceHost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
