using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SmartMarketLibrary.Tests
{
    [TestFixture]
    class SaleTests
    {
        private static Sale _sale=new Sale()
        {
            Date = DateTime.Today,
            Id = 1,
            Seller_Id = 1
        };

        [Test]
        public void CopyConstructor()
        {
            var result=new Sale(_sale);
            Assert.AreEqual(result.Seller_Id, _sale.Seller_Id);
            Assert.AreEqual(result.Date, _sale.Date);
            Assert.AreEqual(result.Id, _sale.Id);
        }

        [Test]
        public void EqualsTrue()
        {
            Assert.IsTrue(_sale.Equals(new Sale(_sale)));
        }

        [Test]
        public void EqualsFalse()
        {
            Assert.IsFalse(_sale.Equals(new Sale()
            {
                Date = DateTime.Today,
                Id = 2,
                Seller_Id = 8
            }));
        }
    }
}
