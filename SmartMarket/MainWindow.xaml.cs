using SmartMarketLibrary;
using System;
using System.Windows;
using System.Windows.Input;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal class EmployeeWrapper : EventArgs
        {
            public Employee EmployeeToWrap;
        }
        internal DatabaseServices Services { get; }
        private Employee _loggedIn;
        private void OnLogin(object sender, EmployeeWrapper user)
        {
            _loggedIn = user.EmployeeToWrap;
            LoginControl.Visibility = Visibility.Hidden;
            if (_loggedIn.Role == 1)
            {



                CashierControl.Initialize(Services, _loggedIn);
                CashierControl.Visibility = Visibility.Visible;
            }
            else
            {
                ManagerControl.Initialize(Services, _loggedIn);
                ManagerControl.Visibility = Visibility.Visible;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Services = Factory.GetServices();

            LoginControl.InitMainWindow(this);
            LoginControl.LoginSuccessful += OnLogin;
            LoginControl.Visibility = Visibility.Visible;

        }


    }

    public class WaitCursor : IDisposable
    {
        public WaitCursor()
        {
            Mouse.OverrideCursor = Cursors.Wait;
        }

        public void Dispose()
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}