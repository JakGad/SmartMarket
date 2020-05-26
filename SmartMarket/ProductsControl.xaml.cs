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
    /// Interaction logic for ProductsControl.xaml
    /// </summary>
    public partial class ProductsControl : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private DatabaseServices _database;
        private Employee _manager;


        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public ProductsControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void Initialize(DatabaseServices database, Employee manager)
        {
            _manager = manager;
            _database = database;
            try
            {
                Products = new ObservableCollection<Product>(_database.GetProducts(x => true));
                Groups = new ObservableCollection<Group>(_database.GetGroups());
                foreach (var prod in Products)
                {
                    prod.Groups = Groups;
                }
            }
            catch (Exception)
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Products.Add(new Product() { Groups = Groups });
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dbProducts = _database.GetProducts(x => true);
                foreach (var product in Products)
                {
                    if (product.Id == 0)
                    {
                        product.ProductGroup_Id = product.Group.Id;
                        product.Group = null;
                        _database.AddProduct(product, _manager);
                    }
                    else
                    {
                        var temp = dbProducts.FirstOrDefault(x => x.Id == product.Id);
                        if (temp != null && !product.Equals(temp))
                        {
                            product.ProductGroup_Id = product.Group.Id;
                            product.Group = null;
                            _database.UpdateProduct(product, _manager);
                        }
                    }
                }

                Products = new ObservableCollection<Product>(_database.GetProducts(x => true));
            }
            catch (Exception)
            {
                ErrorConnectingDialog.IsOpen = true;
            }
        }
    }
}
