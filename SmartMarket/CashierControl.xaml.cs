using SmartMarket.Annotations;
using SmartMarketLibrary;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for CashierControl.xaml
    /// </summary>
    public partial class CashierControl : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<SaleProduct> _productsList;

        public ObservableCollection<SaleProduct> ProductsList
        {
            get => _productsList;
            set
            {
                _productsList = value;
                OnPropertyChanged(nameof(ProductsList));
            }
        }

        public CashierControl()
        {
            InitializeComponent();
            FindDialog.OnSelected += selected;
            DataContext = this;
        }

        private DatabaseServices _database;
        private ProductDelivery _fake;
        private Employee _cashier;
        private void selected(object obj, DeliveryList.DPWrapper wrap)
        {
            if (ProductsList.FirstOrDefault(x => x.Product == wrap.wrapped.Product) == null)
            {
                ProductsList.Add(new SaleProduct() { Product1 = wrap.wrapped.Product1, Product = wrap.wrapped.Product, Price = wrap.wrapped.Product1.Price });
            }
            SelectProductDialog.IsOpen = false;
        }

        public void Initialize(DatabaseServices database, Employee cashier)
        {
            _database = database;
            _cashier = cashier;
            ProductsList = new ObservableCollection<SaleProduct>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddGroupButton_Click(object sender, RoutedEventArgs e)
        {
            FindDialog.Initialize(_database, new ProductDelivery());
            SelectProductDialog.IsOpen = true;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var toAdd = new Sale()
                {
                    Date = DateTime.Now,
                    Employee = _cashier,
                    SaleProducts = ProductsList,
                    Seller_Id = _cashier.Id
                };
                _database.AddSale(toAdd, _cashier);
                ToPayBox.Text = toAdd.SumValue.ToString();
                ToPay.IsOpen = true;
                ProductsList = new ObservableCollection<SaleProduct>();
            }
            catch (Exception exception)
            {
                ErrorConnectingDialog.IsOpen = true;
            }
            
        }
    }
}
