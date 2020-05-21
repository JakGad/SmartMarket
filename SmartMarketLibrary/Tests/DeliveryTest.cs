using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SmartMarketLibrary.Tests
{
    [TestFixture]
    class DeliveryTest
    {
        private static Employee _employee = new Employee()
        {
            Id = 1,
            Login = "LogIn1",
            Name = "Carl Colossus",
            Password = DatabaseServices.GetMd5Hash("CArlOs01"),
            Role = 1

        };
        private static Delivery _delivery = new Delivery()
        {

            Date = new DateTime(2019, 09, 12),
            Id = 1,
            Invoice = "favat014",
            Manager_Id = 1
        };
        [Test]
        public void CopyConstructor()
        {
            var result = new Delivery(_delivery);
            Assert.AreEqual(_delivery.Date, result.Date);
            Assert.AreEqual(_delivery.Id, result.Id);
            Assert.AreEqual(_delivery.Invoice, result.Invoice);
            Assert.AreEqual(_delivery.Manager_Id, result.Manager_Id);
            Assert.AreEqual(_delivery.Products, result.Products);
        }

        [Test]
        public void EqualsTrue()
        {
            Assert.IsTrue(_delivery.Equals(new Delivery(_delivery)));
        }

        [Test]
        public void EqualsFalse()
        {
            Assert.IsFalse(_delivery.Equals(new Delivery()
            {
                Date = new DateTime(2019, 09, 12),
                Id = 2,
                Invoice = "favat014",
                Manager_Id = 5
            }));
        }
    }
}
