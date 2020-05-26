using SmartMarket.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl, INotifyPropertyChanged
    {

        public string Login { get; set; }
        public string Password { get; set; }
        internal event EventHandler<MainWindow.EmployeeWrapper> LoginSuccessful;

        public LoginControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private MainWindow _mainWindow;


        public void InitMainWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }



        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var cursor = new WaitCursor())
            {
                try
                {
                    var user = _mainWindow.Services.Login(Login, FloatingPasswordBox.Password);
                    if (user == null)
                    {

                        WrongLP.IsOpen = true;
                    }
                    else
                    {

                        LoginSuccessful?.Invoke(this, new MainWindow.EmployeeWrapper()
                        {
                            EmployeeToWrap = user
                        });
                    }
                }
                catch (Exception)
                {
                    ErrorConnectingDialog.IsOpen = true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
