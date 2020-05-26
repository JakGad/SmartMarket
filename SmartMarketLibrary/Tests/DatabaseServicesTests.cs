using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;




namespace SmartMarketLibrary.Tests
{
    [TestFixture]
    class EmployeesOperationsTests
    {
        private List<Employee> _employees = new List<Employee>()
        {
            new Employee()
            {
                Id = 1,
                Login = "Romana",
                Password = DatabaseServices.GetMd5Hash("WolFram01"),
                Role = 0
            },
            new Employee()
            {
                Id = 2,
                Login = "Karuzela102",
                Password = DatabaseServices.GetMd5Hash("NUnit01"),
                Role = 1
            }
        };

        private Mock<DbSet<Employee>> _employeesMock;
        private Mock<Entities> _contextMock;
        private DatabaseServices _dbServices;
        private Mock<DbSet<EmployeeChange>> _changesMock;
        private IQueryable<Employee> employees;
        private IQueryable<EmployeeChange> changes;

        [SetUp]
        public void init()
        {
            changes = new List<EmployeeChange>().AsQueryable();

            employees = _employees.Select(x => new Employee(x)).AsQueryable();
            _employeesMock = new Mock<DbSet<Employee>>();
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            _changesMock = new Mock<DbSet<EmployeeChange>>();
            _changesMock.As<IQueryable<EmployeeChange>>().Setup(m => m.Provider).Returns(changes.Provider);
            _changesMock.As<IQueryable<EmployeeChange>>().Setup(m => m.Expression).Returns(changes.Expression);
            _changesMock.As<IQueryable<EmployeeChange>>().Setup(m => m.ElementType).Returns(changes.ElementType);
            _changesMock.As<IQueryable<EmployeeChange>>().Setup(m => m.GetEnumerator()).Returns(changes.GetEnumerator());

            _contextMock = new Mock<Entities>();
            _contextMock.Setup(x => x.Employees).Returns(_employeesMock.Object);
            _contextMock.Setup(x => x.EmployeesChanges).Returns(_changesMock.Object);
            _dbServices = new DatabaseServices(_contextMock.Object);
        }

        [Test]
        public void LoginSucceeded_Login()
        {
            var result = _dbServices.Login("Romana", "WolFram01");
            Assert.IsNotNull(result);
            Assert.AreEqual(_employees[0].Login, result.Login);
            Assert.AreEqual(_employees[0].Password, result.Password);
            Assert.AreEqual(_employees[0].Role, result.Role);
        }

        [Test]
        public void WrongLogin_Login()
        {
            var result = _dbServices.Login("Roman", "WolFram01");
            Assert.IsNull(result);
        }

        [Test]
        public void WrongPassword_Login()
        {
            var result = _dbServices.Login("Romana28", "WolFram");
            Assert.IsNull(result);
        }

        [Test]
        public void GetEmployees()
        {
            var result = _dbServices.GetEmployees();
            Assert.AreEqual(_employees, result);
        }

        [Test]
        public void AddEmployee_Success()
        {
            var toAdd = new Employee()
            {
                Login = "Zbig21",
                Password = DatabaseServices.GetMd5Hash("Har31"),
                Role = 0
            };
            var result = _dbServices.AddEmployee(toAdd, _employees.First(x => x.Role == 0));
            Assert.IsTrue(result);
            _employeesMock.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once);
            _changesMock.Verify(m => m.Add(It.IsAny<EmployeeChange>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        public void AddEmployee_NotUniqueLogin()
        {
            var toAdd = new Employee()
            {
                Login = "Romana",
                Password = DatabaseServices.GetMd5Hash("WolFram01"),
                Role = 1
            };
            var result = _dbServices.AddEmployee(toAdd, _employees.First(x => x.Role == 1));
            Assert.IsFalse(result);
        }


        [Test]
        public void UpdateEmployee_Success()
        {
            var toAdd = _employees[0];
            toAdd.Login = "Kras110";
            var result = _dbServices.UpdateEmployee(toAdd, _employees.First(x => x.Role == 0));
            Assert.IsTrue(result);
            _changesMock.Verify(m => m.Add(It.IsAny<EmployeeChange>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }



    }


    [TestFixture]
    class DeliveryTesting
    {
        private Employee _employee = new Employee()
        {
            Id = 2,
            Login = "Karuzela102",
            Password = DatabaseServices.GetMd5Hash("NUnit01"),
            Role = 0
        };

        private List<Delivery> _deliveries = new List<Delivery>()
        {
            new Delivery()
            {
                Date = new DateTime(2019, 09, 12),
                Id = 1,
                Invoice = "favat014",
                Manager_Id = 2
            },
            new Delivery()
            {
                Date = new DateTime(2018, 09, 12),
                Id = 2,
                Invoice = "favat015",
                Manager_Id = 2
            }
        };

        private List<DeliveriesChanges> _changes = new List<DeliveriesChanges>();
        private DatabaseServices _services;
        private Mock<DbSet<Delivery>> _deliveryMock;
        private Mock<DbSet<DeliveriesChanges>> _changesMock;
        private Mock<Entities> _contextMock;
        private IQueryable<Delivery> deliveries;
        private IQueryable<DeliveriesChanges> changes;
        private IQueryable<Employee> _employees;
        private Mock<DbSet<Employee>> _employeesMock;

        [SetUp]
        public void init()
        {
            deliveries = _deliveries.Select(x => new Delivery(x)).AsQueryable();
            _deliveryMock = new Mock<DbSet<Delivery>>();
            _deliveryMock.As<IQueryable<Delivery>>().Setup(m => m.Provider).Returns(deliveries.Provider);
            _deliveryMock.As<IQueryable<Delivery>>().Setup(m => m.Expression).Returns(deliveries.Expression);
            _deliveryMock.As<IQueryable<Delivery>>().Setup(m => m.ElementType).Returns(deliveries.ElementType);
            _deliveryMock.As<IQueryable<Delivery>>().Setup(m => m.GetEnumerator()).Returns(deliveries.GetEnumerator());

            changes = _changes.AsQueryable();
            _changesMock = new Mock<DbSet<DeliveriesChanges>>();
            _changesMock.As<IQueryable<DeliveriesChanges>>().Setup(m => m.Provider).Returns(changes.Provider);
            _changesMock.As<IQueryable<DeliveriesChanges>>().Setup(m => m.Expression).Returns(changes.Expression);
            _changesMock.As<IQueryable<DeliveriesChanges>>().Setup(m => m.ElementType).Returns(changes.ElementType);
            _changesMock.As<IQueryable<DeliveriesChanges>>().Setup(m => m.GetEnumerator()).Returns(changes.GetEnumerator());

            _employees = new List<Employee>() { _employee }.AsQueryable();
            _employeesMock = new Mock<DbSet<Employee>>();
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(_employees.Provider);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(_employees.Expression);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(_employees.ElementType);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(_employees.GetEnumerator());



            _contextMock = new Mock<Entities>();
            _contextMock.Setup(m => m.Deliveries).Returns(_deliveryMock.Object);
            _contextMock.Setup(m => m.DeliveriesChanges).Returns(_changesMock.Object);
            _contextMock.Setup(m => m.Employees).Returns(_employeesMock.Object);
            _services = new DatabaseServices(_contextMock.Object);
        }


        [Test]
        public void GetDeliveries()
        {
            var result = _services.GetDeliveries();
            Assert.NotNull(result);
            Assert.AreEqual(_deliveries, result);
        }

        

        [Test]
        public void UpdateDelivery_Success()
        {
            var delToUpdate = new Delivery(_deliveries[0]);
            delToUpdate.Manager_Id = 3;
            var result = _services.UpdateDelivery(delToUpdate, _employee);
            Assert.IsTrue(result);
            _changesMock.Verify(m => m.Add(It.IsAny<DeliveriesChanges>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }


        [Test]
        public void UpdateDelivery_CannotFindDelivery()
        {
            var delToUpdate = new Delivery(_deliveries[0]);
            delToUpdate.Manager_Id = 3;
            delToUpdate.Id = 100000;
            var result = _services.UpdateDelivery(delToUpdate, _employee);
            Assert.IsFalse(result);
        }

    }

    [TestFixture]
    class GroupsTesting
    {
        private List<Group> _groupsList = new List<Group>()
        {
            new Group()
            {
                Id = 1,
                Is18 = true,
                Name = "Alcohol"
            },
            new Group()
            {
                Id = 2,
                Is18 = false,
                Name = "Bread"
            }
        };

        private IQueryable<Group> _queryGroups;
        private Mock<DbSet<Group>> _groupsMock;
        private Mock<Entities> _contextMock;
        private DatabaseServices _services;

        [SetUp]
        public void Init()
        {
            _queryGroups = _groupsList.AsQueryable();

            _groupsMock = new Mock<DbSet<Group>>();
            _groupsMock.As<IQueryable<Group>>().Setup(m => m.Provider).Returns(_queryGroups.Provider);
            _groupsMock.As<IQueryable<Group>>().Setup(m => m.Expression).Returns(_queryGroups.Expression);
            _groupsMock.As<IQueryable<Group>>().Setup(m => m.ElementType).Returns(_queryGroups.ElementType);
            _groupsMock.As<IQueryable<Group>>().Setup(m => m.GetEnumerator()).Returns(_queryGroups.GetEnumerator());

            _contextMock = new Mock<Entities>();
            _contextMock.Setup(m => m.Groups).Returns(_groupsMock.Object);
            _services = new DatabaseServices(_contextMock.Object);
        }

        [Test]
        public void GetGroups()
        {
            var result = _services.GetGroups();
            Assert.AreEqual(result, _groupsList);
        }

        [Test]
        public void AddGroup()
        {
            var groupToAdd = new Group()
            {
                Is18 = false,
                Name = "Spices",
            };
            _services.AddGroup(groupToAdd);
            _groupsMock.Verify(m => m.Add(It.IsAny<Group>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        public void UpdateGroup_Accomplished()
        {
            var groupToChange = new Group(_groupsList[0]);
            groupToChange.Name = "Cigarettes";
            var result = _services.UpdateGroup(groupToChange);
            Assert.IsTrue(result);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }


        [Test]
        public void UpdateGroup_IdNotPresent()
        {
            var groupToChange = new Group(_groupsList[0]);
            groupToChange.Name = "Cigarettes";
            groupToChange.Id = 100001;
            var result = _services.UpdateGroup(groupToChange);
            Assert.AreNotEqual(result, 0);
        }
    }

    [TestFixture]
    class ProductsTesting
    {

        private Employee _employee = new Employee()
        {
            Id = 2,
            Login = "Karuzela102",
            Password = DatabaseServices.GetMd5Hash("NUnit01"),
            Role = 0
        };

        private readonly List<Product> _productsList = new List<Product>()
        {
            new Product()
            {
                Availability = true,
                Code = "12321",
                Id = 1,
                Manufacturer = "Gregory House Inc.",
                Margin = (decimal)0.02,
                Name="Vicodine",
                NetPrice = (decimal)40,
                Quantity = 50
            },
            new Product()
            {
                Availability = true,
                Code = "12325",
                Id = 2,
                Manufacturer = "Gregory House Inc.",
                Margin = (decimal)0.02,
                Name="Vicodine2",
                NetPrice = (decimal)40,
                Quantity = 50
            }
        };

        private IQueryable<Product> _productsQueryable;
        private IQueryable<ProductChange> _changesQueryable;
        private Mock<DbSet<Product>> _productsMock;
        private Mock<DbSet<ProductChange>> _changesMock;
        private Mock<Entities> _contextMock;
        private DatabaseServices _services;
        private IQueryable<Employee> _employees;
        private Mock<DbSet<Employee>> _employeesMock;

        [SetUp]
        public void init()
        {
            _productsQueryable = _productsList.AsQueryable();

            _productsMock = new Mock<DbSet<Product>>();
            _productsMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(_productsQueryable.Provider);
            _productsMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(_productsQueryable.Expression);
            _productsMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(_productsQueryable.ElementType);
            _productsMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(_productsQueryable.GetEnumerator());


            _changesQueryable = new List<ProductChange>().AsQueryable();
            _changesMock = new Mock<DbSet<ProductChange>>();
            _changesMock = new Mock<DbSet<ProductChange>>();
            _changesMock.As<IQueryable<ProductChange>>().Setup(m => m.Provider).Returns(_changesQueryable.Provider);
            _changesMock.As<IQueryable<ProductChange>>().Setup(m => m.Expression).Returns(_changesQueryable.Expression);
            _changesMock.As<IQueryable<ProductChange>>().Setup(m => m.ElementType).Returns(_changesQueryable.ElementType);
            _changesMock.As<IQueryable<ProductChange>>().Setup(m => m.GetEnumerator()).Returns(_changesQueryable.GetEnumerator());

            _employees = new List<Employee>() { _employee }.AsQueryable();
            _employeesMock = new Mock<DbSet<Employee>>();
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(_employees.Provider);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(_employees.Expression);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(_employees.ElementType);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(_employees.GetEnumerator());


            _contextMock = new Mock<Entities>();
            _contextMock.Setup(m => m.Products).Returns(_productsMock.Object);
            _contextMock.Setup(m => m.ProductsChanges).Returns(_changesMock.Object);
            _contextMock.Setup(m => m.Employees).Returns(_employeesMock.Object);
            _services = new DatabaseServices(_contextMock.Object);
        }

        [Test]
        public void GetProducts()
        {
            var result = _services.GetProducts((Product a) => a.Availability == true);
            Assert.AreEqual(result, _productsList);
        }

        [Test]
        public void AddProduct()
        {
            var productToAdd = new Product()
            {
                Availability = true,
                Code = "12325",
                Manufacturer = "Gregory House Inc.",
                Margin = (decimal)0.02,
                Name = "Vicodine3",
                NetPrice = (decimal)40,
                Quantity = 50
            };

            var result = _services.AddProduct(productToAdd, _employee);
            Assert.IsTrue(result);
            _changesMock.Verify(m => m.Add(It.IsAny<ProductChange>()), Times.Once);
            _productsMock.Verify(m => m.Add(It.IsAny<Product>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        public void UpdateProduct()
        {
            var productToUpdate = _productsList[0];
            var result = _services.UpdateProduct(productToUpdate, _employee);
            Assert.IsTrue(result);
            _changesMock.Verify(m => m.Add(It.IsAny<ProductChange>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }

    [TestFixture]
    class SalesTesting
    {
        private Employee _employee = new Employee()
        {
            Id = 2,
            Login = "Karuzela102",
            Password = DatabaseServices.GetMd5Hash("NUnit01"),
            Role = 0
        };
        private readonly List<Sale> _salesList = new List<Sale>()
        {
            new Sale()
            {
                Date = DateTime.Today,
                Id=1,
                Seller_Id = 2
            }
        };

        private IQueryable<Sale> _salesQueryable;
        private Mock<DbSet<Sale>> _salesMock;
        private Mock<Entities> _contextMock;
        private DatabaseServices _services;
        private IQueryable<Employee> _employees;
        private Mock<DbSet<Employee>> _employeesMock;

        [SetUp]
        public void init()
        {
            _salesQueryable = _salesList.AsQueryable();

            _salesMock = new Mock<DbSet<Sale>>();
            _salesMock.As<IQueryable<Sale>>().Setup(m => m.Provider).Returns(_salesQueryable.Provider);
            _salesMock.As<IQueryable<Sale>>().Setup(m => m.Expression).Returns(_salesQueryable.Expression);
            _salesMock.As<IQueryable<Sale>>().Setup(m => m.ElementType).Returns(_salesQueryable.ElementType);
            _salesMock.As<IQueryable<Sale>>().Setup(m => m.GetEnumerator()).Returns(_salesQueryable.GetEnumerator());

            _employees = new List<Employee>() { _employee }.AsQueryable();
            _employeesMock = new Mock<DbSet<Employee>>();
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(_employees.Provider);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(_employees.Expression);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(_employees.ElementType);
            _employeesMock.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(_employees.GetEnumerator());


            _contextMock = new Mock<Entities>();
            _contextMock.Setup(m => m.Sales).Returns(_salesMock.Object);
            _contextMock.Setup(m => m.Employees).Returns(_employeesMock.Object);
            _services = new DatabaseServices(_contextMock.Object);
        }

        [Test]
        public void GetSales()
        {
            var result = _services.GetSales(sale => sale.Id == 1);
            Assert.AreEqual(result, _salesList);
        }

        


    }

    [TestFixture]
    class SupplierTesting
    {

        private readonly List<Supplier> _suppliersList = new List<Supplier>()
        {
            new Supplier()
            {
                Id = 1,
                Name="Supply1",
                NIP = "213432123"
            },
            new Supplier()
            {
                Id = 2,
                Name="Supply2",
                NIP = "213432123"
            }
        };


        private IQueryable<Supplier> _suppliersQueryable;
        private Mock<DbSet<Supplier>> _suppliersMock;
        private Mock<Entities> _contextMock;
        private DatabaseServices _services;


        [SetUp]
        public void Init()
        {
            _suppliersQueryable = _suppliersList.AsQueryable();

            _suppliersMock = new Mock<DbSet<Supplier>>();
            _suppliersMock.As<IQueryable<Supplier>>().Setup(m => m.Provider).Returns(_suppliersQueryable.Provider);
            _suppliersMock.As<IQueryable<Supplier>>().Setup(m => m.Expression).Returns(_suppliersQueryable.Expression);
            _suppliersMock.As<IQueryable<Supplier>>().Setup(m => m.ElementType).Returns(_suppliersQueryable.ElementType);
            _suppliersMock.As<IQueryable<Supplier>>().Setup(m => m.GetEnumerator()).Returns(_suppliersQueryable.GetEnumerator());


            _contextMock = new Mock<Entities>();
            _contextMock.Setup(m => m.Suppliers).Returns(_suppliersMock.Object);
            _services = new DatabaseServices(_contextMock.Object);
        }

        [Test]
        public void GetSuppliers()
        {
            var reslut = _services.GetSuppliers();
            Assert.AreEqual(reslut, _suppliersList);
            Assert.AreEqual(reslut, _suppliersList);
        }

        [Test]
        public void AddSupplier()
        {
            var supplierToAdd = new Supplier()
            {
                Name = "Supply3",
                NIP = "213732123"
            };
            _services.AddSupplier(supplierToAdd);
            _suppliersMock.Verify(m => m.Add(It.IsAny<Supplier>()), Times.Once);
            _contextMock.Verify(m => m.SaveChanges());
        }

        [Test]
        public void UpdateSupplier()
        {
            var supplierToUpdate = new Supplier()
            {
                Id = 2,
                Name = "Supply3",
                NIP = "213732123"
            };
            var result = _services.UpdateSupplier(supplierToUpdate);
            Assert.IsTrue(result);
            _contextMock.Verify(m => m.SaveChanges());
        }


    }

}


