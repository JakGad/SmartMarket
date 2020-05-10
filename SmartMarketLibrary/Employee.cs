using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
