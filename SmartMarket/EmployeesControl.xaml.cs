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
    /// Interaction logic for EmployeesControl.xaml
    /// </summary>

    public partial class EmployeesControl : UserControl, INotifyPropertyChanged
    {



        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        public EmployeesControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private Employee _manager;
        private DatabaseServices _database;

        internal void Initialize(DatabaseServices database, Employee manager)
        {
            _database = database;
            _manager = manager;
            Employees = new ObservableCollection<Employee>(database.GetEmployees().Select(x => new Employee(x)));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var emp = new Employee();
            SelectedEmployee = emp;
            _passwordEmployee = emp;
            SetLoginPassword.IsOpen = true;
            PasswordBox.Clear();
            LoginBox.Clear();
        }

        public Employee SelectedEmployee { get; set; }

        private void PasswordButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_passwordEmployee != null)
            {
                _passwordEmployee.Password = DatabaseServices.GetMd5Hash(PasswordBox.Password);
                SetPassword.IsOpen = false;
            }

        }

        private Employee _passwordEmployee;
        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployee != null)
            {
                _passwordEmployee = SelectedEmployee;
                SetPassword.IsOpen = true;
                PasswordBox.Clear();
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {

            using (var cursor = new WaitCursor())
            {
                try
                {
                    var employees = _database.GetEmployees();
                    foreach (var employee in Employees)
                    {
                        if (employee.Id == 0)
                        {
                            if (!_database.AddEmployee(employee, _manager))
                                LoginNotUniqueDialog.IsOpen = true;
                        }
                        else
                        {
                            var toCheck = employees.FirstOrDefault(x => x.Id == employee.Id);
                            if (toCheck != null && !toCheck.Equals(employee))
                                if (!_database.UpdateEmployee(employee, _manager))
                                    LoginNotUniqueDialog.IsOpen = true;
                        }
                    }
                }
                catch (Exception exc)
                {
                    ErrorConnectingDialog.IsOpen = true;
                }
            }
        }

        private void PasswordButton2_OnClick(object sender, RoutedEventArgs e)
        {
            _passwordEmployee.Login = LoginBox.Text;
            _passwordEmployee.Password = DatabaseServices.GetMd5Hash(PasswordBox2.Password);
            SetLoginPassword.IsOpen = false;
            Employees.Add(_passwordEmployee);
        }
    }
}
