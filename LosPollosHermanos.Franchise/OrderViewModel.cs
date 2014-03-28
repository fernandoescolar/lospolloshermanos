using LosPollosHermanos.ServiceContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollosHermanos.Franchise
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public OrderRequest Order { get; set; }

        public OrderViewModel(OrderRequest order)
        {
            this.Order = order;
            this.RaisePropertyChanged("Order");
        }

        private void RaisePropertyChanged(string property)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
