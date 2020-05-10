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
            Seller = new Employee()
            {
                Id = 1,
                Login = "Romana",
                Password = DatabaseServices.GetMd5Hash("WolFram01"),
                Role = RolesEnum.Cashier

            },
            Sold = new List<Tuple<Product, int, decimal>>()
            {
                new Tuple<Product, int, decimal>(new Product()
                {
                    Id = 1,
                    Availability = true,
                    Code = "12325",
                    Manufacturer = "Gregory House Inc.",
                    Margin = (decimal)0.02,
                    Name = "Vicodine3",
                    NetPrice = (decimal)40,
                    Quantity = 50
                }, 5, (decimal)20 )
            }
        };

        [Test]
        public void CopyConstructor()
        {
            var result=new Sale(_sale);
            Assert.AreEqual(result.Seller, _sale.Seller);
            Assert.AreEqual(result.Date, _sale.Date);
            Assert.AreEqual(result.Id, _sale.Id);
            Assert.AreEqual(result.Sold, _sale.Sold);
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
                Seller = new Employee()
                {
                    Id = 1,
                    Login = "Romana",
                    Password = DatabaseServices.GetMd5Hash("WolFram01"),
                    Role = RolesEnum.Cashier

                },
                Sold = new List<Tuple<Product, int, decimal>>()
                {
                    new Tuple<Product, int, decimal>(new Product()
                    {
                        Id = 1,
                        Availability = true,
                        Code = "12325",
                        Manufacturer = "Gregory House Inc.",
                        Margin = (decimal)0.02,
                        Name = "Vicodine3",
                        NetPrice = (decimal)40,
                        Quantity = 50
                    }, 5, (decimal)20 )
                }
            }));
        }
    }
}
