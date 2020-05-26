using SmartMarket.Annotations;
using SmartMarketLibrary;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for SalesControl.xaml
    /// </summary>
    public partial class SalesControl : UserControl, INotifyPropertyChanged
    {
        public SalesControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private ObservableCollection<Sale> _sales;

        public ObservableCollection<Sale> Sales
        {
            get => _sales;
            set
            {
                _sales = value;
                OnPropertyChanged(nameof(Sales));
            }
        }

        private DatabaseServices _database;

        public Sale Selected { get; set; }

        private void ProductsView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Selected != null)
            {
                SaleListDialog.Initialize(Selected);
                SaleDialog.IsOpen = true;
            }
        }

        public void Initialize(DatabaseServices database)
        {
            _database = database;
            try
            {
                Sales = new ObservableCollection<Sale>(database.GetSales(x => true));
            }
            catch (Exception e)
            {
                ErrorConnectingDialog.IsOpen = true;
            }
        }





        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
