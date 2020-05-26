using SmartMarket.Annotations;
using SmartMarketLibrary;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for DeliveryList.xaml
    /// </summary>
    public partial class DeliveryList : UserControl, INotifyPropertyChanged
    {
        public DeliveryList()
        {
            InitializeComponent();
            DataContext = this;
        }

        private DatabaseServices _database;
        private Delivery _toChange;
        private ObservableCollection<ProductDelivery> _delList;
        public ObservableCollection<ProductDelivery> DelList
        {
            get => _delList;
            set
            {
                _delList = value;
                OnPropertyChanged(nameof(DelList));
            }

        }
        public void Initialize(Delivery toChange, DatabaseServices database)
        {
            DelList = new ObservableCollection<ProductDelivery>(toChange.ProductDeliveries);
            _database = database;
            _toChange = toChange;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        internal class DPWrapper : EventArgs
        {
            public ProductDelivery wrapped;
        }

    }
}
