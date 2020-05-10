using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SmartMarketLibrary.Tests
{
    [TestFixture]
    class EmployeeTests
    {
        private static Employee _employee = new Employee()
        {
            Id = 1,
            Login = "LogIn1",
            Name = "Carl Colossus",
            Password = DatabaseServices.GetMd5Hash("CArlOs01"),
            Role = RolesEnum.Cashier

        };
        [Test]
        public void CopyConstructor()
        {
            var result=new Employee(_employee);
            Assert.AreEqual(result.Id, _employee.Id);
            Assert.AreEqual(result.Login, _employee.Login);
            Assert.AreEqual(result.Password, _employee.Password);
            Assert.AreEqual(result.Role, _employee.Role);
        }

        [Test]
        public void EqualsTrue()
        {
            Assert.IsTrue(_employee.Equals(new Employee(_employee)));
        }

        [Test]
        public void EqualsFalse()
        {
            Assert.IsFalse(_employee.Equals(new Employee()
            {
                Id = 2,
                Login = "LogIn1",
                Name = "Carl Colossus",
                Password = DatabaseServices.GetMd5Hash("CArlOs01"),
                Role = RolesEnum.Cashier
            }));
        }
    }
}
