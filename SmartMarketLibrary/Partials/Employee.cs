using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    partial class Employee
    {
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
