using FirstFloor.ModernUI.Presentation;
using LosPollosHermanos.Infrastructure;
using LosPollosHermanos.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LosPollosHermanos.Franchise
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        private static Orders currentInstance;
        private readonly Timer timer;
        private readonly List<OrderRequest> currentOrders = new List<OrderRequest>();

        public Orders()
        {
            currentInstance = this;
            InitializeComponent();
            this.timer = new Timer(OnReloadCallback, null, new TimeSpan(0), TimeSpan.FromSeconds(10));
        }

        public static void DeleteOrder(OrderRequest order)
        {
            if (currentInstance != null)
            {
                currentInstance.currentOrders.Remove(order);
                currentInstance.LoadLinks();
            }
        }

        public static OrderRequest GetOrder(int id)
        {
            if (currentInstance != null)
            {
                return currentInstance.currentOrders.FirstOrDefault(o => o.OrderId == id);
            }

            return null;
        }

        private void OnReloadCallback(object state)
        {
            this.LoadOrders();
            this.LoadLinks();
        }

        private void LoadOrders()
        {
            using (var proxy = new Proxy<IOrdersService>("OrdersService"))
            {
                currentOrders.AddRange(proxy.Call(s => s.GetPendingOrders()));
            }
        }

        private void LoadLinks()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.tab.Links.Clear();
                currentOrders.Select(o => new Link { DisplayName = "Order #" + o.OrderId, Source = new Uri("Order.xaml#" + o.OrderId, UriKind.Relative) }).ToList().ForEach(this.tab.Links.Add);
            }), DispatcherPriority.Normal, null);
        }
    }
}
