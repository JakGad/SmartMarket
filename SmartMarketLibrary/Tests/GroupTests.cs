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
    class GroupTests
    {
        private static Group _group = new Group()
        {
            Id = 1,
            Is18 = true,
            Name = "SomeName",
            Vat = (decimal)0.18
        };
        [Test]
        public void CopyConstructor()
        {
            var result=new Group(_group);
            Assert.AreEqual(result.Id, _group.Id);
            Assert.AreEqual(result.Is18, _group.Is18);
            Assert.AreEqual(result.Name, _group.Name);
            Assert.AreEqual(result.Vat, _group.Vat);
        }

        [Test]
        public void EqualsTrue()
        {
            Assert.IsTrue(_group.Equals(new Group(_group)));
        }

        [Test]
        public void EqualsFalse()
        {
            Assert.IsFalse(_group.Equals(new Group()
            {
                Id = 2,
                Is18 = true,
                Name = "SomeName",
                Vat = (decimal)0.18
            }));
        }
    }
}
