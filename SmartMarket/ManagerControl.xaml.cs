using SmartMarket.Annotations;
using SmartMarketLibrary;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for ManagerControl.xaml
    /// </summary>
    public partial class ManagerControl : UserControl, INotifyPropertyChanged
    {
        public ManagerControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private Employee _manager;
        private DatabaseServices _database;

        public string ManagerHello
        {
            get => _manager?.Name ?? "";
        }


        public void Initialize(DatabaseServices database, Employee manager)
        {
            _manager = manager;
            OnPropertyChanged(nameof(ManagerHello));
            _database = database;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MakeAllTabsHidden()
        {
            EmployeesControlTab.Visibility = Visibility.Hidden;
            GroupsControlTab.Visibility = Visibility.Hidden;
            SuppliersControlTab.Visibility = Visibility.Hidden;
            ProductsTab.Visibility = Visibility.Hidden;
            SalesTab.Visibility = Visibility.Hidden;
            DeliveriesTab.Visibility = Visibility.Hidden;
        }

        private void EmployeesButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var cursor = new WaitCursor())
            {
                MakeAllTabsHidden();
                EmployeesControlTab.Initialize(_database, _manager);
                EmployeesControlTab.Visibility = Visibility.Visible;

            }

        }

        private void GroupsButton_Click(object sender, RoutedEventArgs e)
        {
            using (var cursor = new WaitCursor())
            {
                MakeAllTabsHidden();
                GroupsControlTab.Initialize(_database);
                GroupsControlTab.Visibility = Visibility.Visible;

            }
        }

        private void SupplirsButton_Click(object sender, RoutedEventArgs e)
        {
            using (var cursor = new WaitCursor())
            {
                MakeAllTabsHidden();
                SuppliersControlTab.Initialize(_database);
                SuppliersControlTab.Visibility = Visibility.Visible;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var cursor = new WaitCursor())
            {
                MakeAllTabsHidden();
                ProductsTab.Initialize(_database, _manager);
                ProductsTab.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var cursor = new WaitCursor())
            {
                MakeAllTabsHidden();
                SalesTab.Initialize(_database);
                SalesTab.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MakeAllTabsHidden();
            DeliveriesTab.Initialize(_database, _manager);
            DeliveriesTab.Visibility = Visibility.Visible;
        }
    }
}
