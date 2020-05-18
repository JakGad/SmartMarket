using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SmartMarketLibrary.Tests
{
    [TestFixture]
    class SupplierTests
    {
        private static Supplier _supplier = new Supplier()
        {

            Id = 1,
            Name = "Supply1",
            NIP = "213432123"

        };
        [Test]
        public void CopyConstructor()
        {
            var result=new Supplier(_supplier);
            Assert.AreEqual(result.Id, _supplier.Id);
            Assert.AreEqual(result.Name, _supplier.Name);
            Assert.AreEqual(result.NIP, _supplier.NIP);
        }

        [Test]
        public void EqualsTrue()
        {
            Assert.IsTrue(_supplier.Equals(new Supplier(_supplier)));
        }

        [Test]
        public void EqualsFalse()
        {
            Assert.IsFalse(_supplier.Equals(new Supplier()
            {
                Id = 2,
                Name = "Supply1",
                NIP = "213432123"
            }));
        }
    }
}
