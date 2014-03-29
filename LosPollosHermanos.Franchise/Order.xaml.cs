using FirstFloor.ModernUI.Windows;
using LosPollosHermanos.Infrastructure;
using LosPollosHermanos.ServiceContracts;
using System.Windows.Controls;

namespace LosPollosHermanos.Franchise
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : UserControl, IContent
    {
        private OrderRequest order;
        public Order()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            var orderId = int.Parse(e.Fragment);
            this.Load(orderId);
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            var parts = e.Source.OriginalString.Split('#');
            var orderId = int.Parse(parts[parts.Length - 1]);
            this.Load(orderId);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }

        private void Load(int orderId)
        {
            this.order = Orders.GetOrder(orderId);
            this.DataContext = order;
            stop.IsEnabled = start.IsEnabled = true;
        }

        private void OnStartClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Orders.UpdateOrder(order, true, false);

            start.IsEnabled = false;
        }

        private void OnDoneClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Orders.UpdateOrder(order, true, true);

            start.IsEnabled = false;
            stop.IsEnabled = false;
            Orders.DeleteOrder(this.order);
        }
    }
}
