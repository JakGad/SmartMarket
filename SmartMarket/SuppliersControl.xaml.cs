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
    /// Interaction logic for SuppliersControl.xaml
    /// </summary>
    public partial class SuppliersControl : UserControl, INotifyPropertyChanged
    {
        public SuppliersControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private DatabaseServices _database;
        internal void Initialize(DatabaseServices database)
        {
            _database = database;
            try
            {
                Suppliers = new ObservableCollection<Supplier>(_database.GetSuppliers());
            }
            catch (Exception)
            {
                ErrorConnectingDialog.IsOpen = true;
            }
        }

        private ObservableCollection<Supplier> _suppliers;
        public ObservableCollection<Supplier> Suppliers
        {
            get => _suppliers;
            set
            {
                _suppliers = value;
                OnPropertyChanged(nameof(Suppliers));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddGroupButton_Click(object sender, RoutedEventArgs e)
        {
            Suppliers.Add(new Supplier());
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var temp = _database.GetSuppliers();
                foreach (var supplier in Suppliers)
                {
                    if (supplier.Id == 0)
                    {
                        _database.AddSupplier(supplier);
                    }
                    else
                    {
                        var sup = temp.FirstOrDefault(x => x.Id == supplier.Id);
                        if (sup != null && !sup.Equals(supplier))
                        {
                            _database.UpdateSupplier(supplier);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorConnectingDialog.IsOpen = true;
            }

        }
    }
}
