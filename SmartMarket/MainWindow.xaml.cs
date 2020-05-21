using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SmartMarketLibrary;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var ds=new DatabaseServices(new DBModel());
            var emp=ds.Login("ADDmiN", "Hassloo12");
            ds.AddEmployee(new Employee()
            {
                Login = "abram12",
                Name = "Julia Abrams",
                Password = DatabaseServices.GetMd5Hash("hass13"),
                Role = RolesEnum.Cashier
            }, emp);
        }
    }
}
