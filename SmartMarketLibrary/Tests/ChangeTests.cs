using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SmartMarketLibrary.Tests
{
    [TestFixture]
    class ChangeTests
    {
        private static Employee _employee = new Employee()
        {
            Id = 1,
            Login = "LogIn1",
            Name = "Carl Colossus",
            Password = DatabaseServices.GetMd5Hash("CArlOs01"),
            Role = 1

        };
        [Test]
        public void Constructor()
        {
            var result = new EmployeeChange(_employee, _employee,  "SomeDetails");
            Assert.IsTrue(result.Date - DateTime.Now < new TimeSpan(0, 0, 0, 2));
            Assert.AreEqual(result.Employee, _employee);
            Assert.AreEqual(result.Employee1, _employee);
            Assert.AreEqual(result.Details, "SomeDetails");
        }

        [Test]
        public void CopyConstructor()
        {
            var temp = new EmployeeChange(_employee,_employee,  "SomeDetails");
            temp.Id = 2;
            var result = new EmployeeChange(temp);
            Assert.AreEqual(result.Date, temp.Date);
            Assert.AreEqual(result.Employee, temp.Employee);
            Assert.AreEqual(result.Details, temp.Details);
            Assert.AreEqual(result.Id, temp.Id);
        }

        private static EmployeeChange _change = new EmployeeChange(_employee, _employee, "Det1");


        [Test]
        public void EqualsTrue()
        {
            Assert.IsTrue(_change.Equals(new EmployeeChange(_change)));
        }

        [Test]
        public void EqualsFalse()
        {
            Assert.IsFalse(_change.Equals(new EmployeeChange(_employee, _employee, "Det2")));
        }
    }
}
