using FirstFloor.ModernUI.Windows;
using LosPollosHermanos.Infrastructure;
using LosPollosHermanos.ServiceContracts;
using System.Linq;
using System.Windows.Controls;

namespace LosPollosHermanos.Franchise
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Product : UserControl, IContent
    {
        private AvailableProduct product;
        public Product()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            var productId = int.Parse(e.Fragment);
            this.Load(productId);
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            var parts = e.Source.OriginalString.Split('#');
            var productId = int.Parse(parts[parts.Length - 1]);
            this.Load(productId);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }

        private void Load(int productId)
        {
            this.product = Stock.AvailableProducts.FirstOrDefault(o => o.Id == productId);
            this.DataContext = product;
        }

        private void OnStartClick(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }

        private void OnDoneClick(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }
    }
}
