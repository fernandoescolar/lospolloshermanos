using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using LosPollosHermanos.Infrastructure;
using LosPollosHermanos.ServiceContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly Timer timer;

        internal readonly static List<OrderRequest> CurrentOrders = new List<OrderRequest>();

        public Orders()
        {
            InitializeComponent();
            this.timer = new Timer(OnReloadCallback, null, new TimeSpan(0), TimeSpan.FromSeconds(10));
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
                CurrentOrders.AddRange(proxy.Call(s => s.GetPendingOrders()));
            }
        }

        private void LoadLinks()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.tab.Links.Clear();
                CurrentOrders.Select(o => new Link { DisplayName = "Order #" + o.OrderId, Source = new Uri("Order.xaml#" + o.OrderId, UriKind.Relative) }).ToList().ForEach(this.tab.Links.Add);
            }), DispatcherPriority.Normal, null);
        }
    }
}
