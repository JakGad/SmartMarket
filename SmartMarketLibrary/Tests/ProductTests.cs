using NUnit.Framework;

namespace SmartMarketLibrary.Tests
{
    [TestFixture]
    class ProductTests
    {
        private static Product _product = new Product()
        {
            Availability = true,
            Code = "12321",
            Id = 1,
            Manufacturer = "Gregory House Inc.",
            Margin = (decimal)0.02,
            Name = "Vicodine",
            NetPrice = (decimal)40,
            Quantity = 50,
            ProductGroup_Id = 1
        };

        [Test]
        public void CopyConstructor()
        {
            var result = new Product(_product);
            Assert.AreEqual(result.Availability, _product.Availability);
            Assert.AreEqual(result.Code, _product.Code);
            Assert.AreEqual(result.Id, _product.Id);
            Assert.AreEqual(result.Margin, _product.Margin);
            Assert.AreEqual(result.Name, _product.Name);
            Assert.AreEqual(result.NetPrice, _product.NetPrice);
            Assert.AreEqual(result.Quantity, _product.Quantity);
            Assert.AreEqual(result.ProductGroup_Id, _product.ProductGroup_Id);
        }


        [Test]
        public void EqualsTrue()
        {
            Assert.IsTrue(_product.Equals(new Product(_product)));
        }

        [Test]
        public void EqualsFalse()
        {
            Assert.IsFalse(_product.Equals(new Product()
            {
                Availability = true,
                Code = "12321",
                Id = 2,
                Manufacturer = "Gregory House Inc.",
                Margin = (decimal)0.02,
                Name = "Vicodine",
                NetPrice = (decimal)40,
                Quantity = 50,
                ProductGroup_Id = 1
            }));
        }
    }
}
