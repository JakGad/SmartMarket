using System.Windows;
using System.Windows.Controls;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for QuitMinimize.xaml
    /// </summary>
    public partial class QuitMinimize : UserControl
    {
        public QuitMinimize()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
