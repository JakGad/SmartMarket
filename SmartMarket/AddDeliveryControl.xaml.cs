using SmartMarket.Annotations;
using SmartMarketLibrary;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for AddDeliveryControl.xaml
    /// </summary>
    public partial class AddDeliveryControl : UserControl, INotifyPropertyChanged
    {
        private DatabaseServices _database;
        private Employee _manager;
        private string _invoice;
        public string Invoice
        {
            get => _invoice;
            set
            {
                _invoice = value;
                OnPropertyChanged(nameof(Invoice));
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public Supplier Supplier { get; set; }
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

        public AddDeliveryControl()
        {
            InitializeComponent();
            DataContext = this;
            SelectDialog.OnSelected += OnSelectedProduct;
        }

        public void Initialize(DatabaseServices database, Employee manager)
        {
            _database = database;
            _manager = manager;
            DelList = new ObservableCollection<ProductDelivery>();
            Date = DateTime.Now;
            Name = "";
            Invoice = "";
            try
            {
                Suppliers = new ObservableCollection<Supplier>(_database.GetSuppliers());
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

        public event EventHandler<EventArgs> OnSavedEventHandler;

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _database.AddDelivery(new Delivery()
                {
                    Date = Date,
                    ProductDeliveries = DelList,
                    Employee = _manager,
                    Invoice = Invoice,
                    Manager_Id = _manager.Id,
                    SupplierId = Supplier.Id
                }, _manager);
                OnSavedEventHandler?.Invoke(this, null);
            }
            catch (Exception)
            {
                ErrorConnectingDialog.IsOpen = true;
            }

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            SelectDialog.Initialize(_database, new ProductDelivery());
            SelectProductDialog.IsOpen = true;
        }

        private void OnSelectedProduct(object o, DeliveryList.DPWrapper wrapper)
        {
            DelList.Add(wrapper.wrapped);
            SelectProductDialog.IsOpen = false;
        }
    }
}
