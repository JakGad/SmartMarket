using SmartMarket.Annotations;
using SmartMarketLibrary;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for DeliveriesControl.xaml
    /// </summary>
    public partial class DeliveriesControl : UserControl, INotifyPropertyChanged
    {
        private DatabaseServices _database;
        private Employee _manager;
        private ObservableCollection<Delivery> _deliveries;

        public ObservableCollection<Delivery> Deliveries
        {
            get => _deliveries;
            set
            {
                _deliveries = value;
                OnPropertyChanged(nameof(Deliveries));
            }
        }

        public Delivery Selected { get; set; }


        public DeliveriesControl()
        {
            InitializeComponent();
            DataContext = this;
            addDelControl.OnSavedEventHandler += OnSaved;
        }

        public void Initialize(DatabaseServices database, Employee manager)
        {
            _database = database;
            _manager = manager;
            try
            {
                Deliveries = new ObservableCollection<Delivery>(database.GetDeliveries());


            }
            catch (Exception e)
            {
                ErrorConnectingDialog.IsOpen = true;
            }
        }


        private void ProductsView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lstDialog.Initialize(Selected, _database);
            DeliveryLDialog.IsOpen = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            addDelControl.Initialize(_database, _manager);
            AddDeliveryDilaog.IsOpen = true;
        }

        private void OnSaved(object o, EventArgs e)
        {
            try
            {
                Deliveries = new ObservableCollection<Delivery>(_database.GetDeliveries());
            }
            catch (Exception exception)
            {
                ErrorConnectingDialog.IsOpen = true;
            }

            AddDeliveryDilaog.IsOpen = false;
        }
    }
}
