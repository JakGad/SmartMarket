using SmartMarket.Annotations;
using SmartMarketLibrary;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for FindProductControl.xaml
    /// </summary>
    public partial class FindProductControl : UserControl, INotifyPropertyChanged
    {
        public FindProductControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private DatabaseServices _database;
        private Product _toSet;
        private ObservableCollection<Product> _allProducts;
        private ObservableCollection<Product> _selectedProducts;

        public ObservableCollection<Product> Products
        {
            get => _selectedProducts;
            set
            {
                _selectedProducts = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        public string NameBeg { get; set; }
        public Product Selected { get; set; }
        private ProductDelivery _pd;
        public void Initialize(DatabaseServices database, ProductDelivery toSet)
        {
            _database = database;
            _toSet = toSet.Product1;
            _pd = toSet;
            try
            {
                _allProducts = new ObservableCollection<Product>(_database.GetProducts(x => true));
                Products = _allProducts;
            }
            catch (Exception e)
            {
                ErrorConnectingDialog.IsOpen = true;
            }
        }


        internal event EventHandler<DeliveryList.DPWrapper> OnSelected;






        private void ProductsView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Selected != null)
            {
                _pd.Product1 = Selected;
                _pd.Product = Selected.Id;
                OnSelected?.Invoke(this, new DeliveryList.DPWrapper() { wrapped = _pd });
            }
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Products = new ObservableCollection<Product>(_allProducts.Where(x => x.Name.Contains(NameBeg)).ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
