﻿using System;
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
            Role = RolesEnum.Cashier

        };
        private static Delivery _delivery = new Delivery()
        {
            
                Date = new DateTime(2019, 09, 12),
                Id = 1,
                Invoice = "favat014",
                Manager = new Employee()
                {
                    Id = 2,
                    Login = "Karuzela102",
                    Password = DatabaseServices.GetMd5Hash("NUnit01"),
                    Role = RolesEnum.Manager
                },
                Products = new List<Tuple<Product, int, decimal>>()
                {
                    new Tuple<Product, int, decimal>(new Product(), 5, (decimal) 3.20)
                }
            };
        [Test]
        public void CopyConstructor()
        {
            var result=new Delivery(_delivery);
            Assert.AreEqual(result.Date, _delivery.Date);
            Assert.AreEqual(result.Id, _delivery.Id);
            Assert.AreEqual(result.Invoice, _delivery.Invoice);
            Assert.AreEqual(result.Manager, _delivery.Manager);
            Assert.AreEqual(result.Products, _delivery.Products);
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
                Manager = new Employee()
                {
                    Id = 2,
                    Login = "Karuzela102",
                    Password = DatabaseServices.GetMd5Hash("NUnit01"),
                    Role = RolesEnum.Manager
                },
                Products = new List<Tuple<Product, int, decimal>>()
                {
                    new Tuple<Product, int, decimal>(new Product(), 5, (decimal) 3.20)
                }
            }));
        }
    }
}