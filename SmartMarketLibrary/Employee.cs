using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMarketLibrary.Tests;

namespace SmartMarketLibrary
{
    public enum RolesEnum
    {
        Manager, 
        Cashier
    }

    public class Employee
    {
        public int Id { get; set; }
        private List<Change> _changes;

        public string Name { get; set; }
        public RolesEnum Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IReadOnlyList<Change> Changes { get=>_changes; }

        public Employee()
        {
        }

        public Employee(Employee toCopy)
        {
            Id = toCopy.Id;
            _changes = toCopy._changes;
            Name = toCopy.Name;
            Login = toCopy.Login;
            Password = toCopy.Password;
            Role = toCopy.Role;
        }

        public override bool Equals(object obj)
        {
            if (obj is Employee employee)
            {
                return employee.Id == Id && employee._changes!=null?employee._changes.Equals(_changes):_changes==null && employee.Name == Name &&
                       employee.Role == Role && employee.Login == Login && employee.Password == Password;
            }

            return false;
        }
    }
}
