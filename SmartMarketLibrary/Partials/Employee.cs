using System.Collections.ObjectModel;

namespace SmartMarketLibrary
{
    partial class Employee
    {
        public ObservableCollection<string> Rol { get; set; } = new ObservableCollection<string>() { "Manager", "Cashier" };
        public string RoleS
        {
            get
            {
                if (Role == 0) return "Manager";
                return "Cashier";
            }
            set
            {
                var temp = value;
                if (temp.EndsWith("Manager"))
                {
                    Role = 0;
                }
                else
                {
                    Role = 1;
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public Employee(Employee toCopy)
        {
            Name = toCopy.Name;
            Role = toCopy.Role;
            Login = toCopy.Login;
            Password = toCopy.Password;
            Id = toCopy.Id;
            Deliveries = toCopy.Deliveries;
            DeliveriesChanges = toCopy.DeliveriesChanges;
            EmployeesChanges = toCopy.EmployeesChanges;
            EmployeesChanges1 = toCopy.EmployeesChanges1;
            ProductsChanges = toCopy.ProductsChanges;
            Sales = toCopy.Sales;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Employee employee)
            {
                return employee.Name == Name && employee.Role == Role && employee.Login == Login &&
                       employee.Password == Password && employee.Id == Id;
            }

            return false;
        }

        public void set(Employee toCopy)
        {
            Name = toCopy.Name;
            Role = toCopy.Role;
            Login = toCopy.Login;
            Password = toCopy.Password;
        }
    }
}
