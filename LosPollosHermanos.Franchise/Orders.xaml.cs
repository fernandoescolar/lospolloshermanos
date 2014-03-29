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
        private readonly List<OrderRequest> currentOrders = new List<OrderRequest>();
        private readonly ServiceBusTopicHelper helper;

        public Orders()
        {
            currentInstance = this;
            InitializeComponent();
            this.helper = ServiceBusTopicHelper.Setup(SubscriptionInitializer.Initialize());
            this.LoadOrders();
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

        public static void UpdateOrder(OrderRequest order, bool isOrdered, bool isDelivered)
        {
            if (currentInstance != null)
            {
                currentInstance.helper.Publish<OrderRequest>(order, (m) =>
                {
                    m.Properties["IsOrdered"] = isOrdered;
                    m.Properties["IsDelivered"] = isDelivered;
                });
            }
        }

        private void LoadOrders()
        {
            helper.Subscribe<OrderRequest>((order) =>
                {
                    currentOrders.Add(order);
                    this.LoadLinks();
                }
                , "(IsOrdered = false) AND (IsDelivered = false)",
                "Orders"
            );
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
