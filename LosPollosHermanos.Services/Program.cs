using System;
using System.ServiceModel;

namespace LosPollosHermanos.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            var ordersServiceHost = new ServiceHost(typeof (OrdersService));
            var productsServiceHost = new ServiceHost(typeof(ProductsService));

            try
            {
                ordersServiceHost.Open();
                //productsServiceHost.Open();

                Console.WriteLine("Press intro to exit");
                Console.ReadLine();

                ordersServiceHost.Close();
                //productsServiceHost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
