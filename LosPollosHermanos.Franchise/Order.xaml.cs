using FirstFloor.ModernUI.Windows;
using LosPollosHermanos.ServiceContracts;
using System.Linq;
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
            this.order = Orders.CurrentOrders.FirstOrDefault(o => o.OrderId == orderId);
            this.DataContext = order;
        }

        private void OnStartClick(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void OnDoneClick(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
