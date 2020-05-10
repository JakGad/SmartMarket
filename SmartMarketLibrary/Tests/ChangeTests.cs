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
            Role = RolesEnum.Cashier

        };
        [Test]
        public void Constructor()
        {
            var result = new Change(_employee, "SomeDetails");
            Assert.IsTrue(result.Date - DateTime.Now < new TimeSpan(0, 0, 0, 2));
            Assert.AreEqual(result.Changing, _employee);
            Assert.AreEqual(result.Details, "SomeDetails");
        }

        [Test]
        public void CopyConstructor()
        {
            var temp = new Change(_employee, "SomeDetails");
            temp.Id = 2;
            var result = new Change(temp);
            Assert.AreEqual(result.Date, temp.Date);
            Assert.AreEqual(result.Changing, temp.Changing);
            Assert.AreEqual(result.Details, temp.Details);
            Assert.AreEqual(result.Id, temp.Id);
        }

        private static Change _change = new Change()
        {
            Changing = _employee,
            Date = DateTime.Today,
            Details = "Some details",
            Id = 1
        };


        [Test]
        public void EqualsTrue()
        {
            Assert.IsTrue(_change.Equals(new Change(_change)));
        }

        [Test]
        public void EqualsFalse()
        {
            Assert.IsFalse(_change.Equals( new Change()
            {
                Changing = _employee,
                Date = DateTime.Today,
                Details = "Some details",
                Id = 2
            }));
        }
    }
}
