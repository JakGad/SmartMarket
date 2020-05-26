using SmartMarket.Annotations;
using SmartMarketLibrary;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for SaleList.xaml
    /// </summary>
    public partial class SaleList : UserControl, INotifyPropertyChanged
    {
        public SaleList()
        {
            InitializeComponent();
            DataContext = this;
        }

        private ObservableCollection<SaleProduct> _saleList;
        public ObservableCollection<SaleProduct> Salelist
        {
            get => _saleList;
            set
            {
                _saleList = value;
                OnPropertyChanged(nameof(Salelist));
            }
        }

        public void Initialize(Sale sp)
        {
            Salelist = new ObservableCollection<SaleProduct>(sp.SaleProducts);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
