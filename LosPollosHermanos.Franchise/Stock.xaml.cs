using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using LosPollosHermanos.Infrastructure;
using LosPollosHermanos.ServiceContracts;

namespace LosPollosHermanos.Franchise
{
    public partial class Stock : UserControl, INotifyPropertyChanged
    {
        private readonly Timer timer;

        public event PropertyChangedEventHandler PropertyChanged;

        public Product[] CurrentProducts { get; set; }

        public Stock()
        {
            DataContext = this;
            InitializeComponent();
            this.timer = new Timer(OnReloadCallback, null, new TimeSpan(0), TimeSpan.FromSeconds(10));
        }

        private void OnReloadCallback(object state)
        {
            using (var proxy = new Proxy<IProductsService>("ProductsService"))
            {
                this.CurrentProducts = proxy.Call(s => s.GetAllProducts());
                this.OnPropertyChanged("CurrentProducts");
            }
        }

        private void OnPropertyChanged(string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnCheckboxClick(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox) sender;
            var product = (Product) checkbox.DataContext;

            using (var proxy = new Proxy<IProductsService>("ProductsService"))
            {
                proxy.Call(s => s.UpdateProductStock(new ProductAvailabilityRequest
                                                     {
                                                         IsAvailable = checkbox.IsChecked.HasValue && checkbox.IsChecked.Value,
                                                         ProductId = product.Id
                                                     }));
            }
        }
    }
}
