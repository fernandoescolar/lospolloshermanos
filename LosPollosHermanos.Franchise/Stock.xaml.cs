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
    public partial class Stock : UserControl
    {
        private readonly Timer timer;

        internal readonly static List<AvailableProduct> AvailableProducts = new List<AvailableProduct>();

        public Stock()
        {
            InitializeComponent();
            this.timer = new Timer(OnReloadCallback, null, new TimeSpan(0), TimeSpan.FromSeconds(10));
        }

        private void OnReloadCallback(object state)
        {
            this.LoadAvailableProducts();
            this.LoadLinks();
        }

        private void LoadAvailableProducts()
        {
            using (var proxy = new Proxy<IProductsService>("ProductsService"))
            {
                AvailableProducts.AddRange(proxy.Call(s => s.GetAvailableProducts()));
            }
        }

        private void LoadLinks()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.tab.Links.Clear();
                AvailableProducts.Select(o => new Link { DisplayName = o.Name, Source = new Uri("Product.xaml#" + o.Id, UriKind.Relative) }).ToList().ForEach(this.tab.Links.Add);
            }), DispatcherPriority.Normal, null);
        }
    }
}
