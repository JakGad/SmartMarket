using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SmartMarketLibrary
{
    public class DatabaseServices : IDisposable
    {
        public Entities _model;

        public DatabaseServices(Entities model)
        {
            _model = model;
        }

        public Employee Login(string login, string password)
        {
            var passwd = DatabaseServices.GetMd5Hash(password);
            var toRet = _model.Employees.FirstOrDefault(x => x.Login == login && x.Password == passwd);
            return toRet;
        }

        public static string GetMd5Hash(string input)
        {
            var hasher = MD5.Create();
            var data = hasher.ComputeHash(Encoding.Default.GetBytes(input));
            var builder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public List<Employee> GetEmployees()
        {
            return _model.Employees.ToList().Select(x => new Employee(x)).ToList();
        }

        public bool AddEmployee(Employee toAdd, Employee currentAdmin)
        {
            bool result = currentAdmin != null &&
                          _model.Employees.FirstOrDefault(x =>
                                  x.Id == currentAdmin.Id && x.Login == currentAdmin.Login) != null && _model.Employees.FirstOrDefault(x => x.Login == toAdd.Login) == null;
            if (result)
            {
                try
                {
                    _model.Employees.Add(toAdd);
                    var change = new EmployeeChange(currentAdmin, toAdd, "User created");
                    _model.EmployeesChanges.Add(change);
                    _model.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _model.Entry(_model.Employees).CurrentValues.SetValues(_model.Entry(_model.Employees).OriginalValues);
                    _model.Entry(_model.EmployeesChanges).CurrentValues.SetValues(_model.Entry(_model.EmployeesChanges).OriginalValues);
                    throw;
                }

            }
            else
            {
                return false;
            }

        }

        public bool UpdateEmployee(Employee toUpdate, Employee currentAdmin)
        {
            var result = _model.Employees.FirstOrDefault(x =>
                x.Id == currentAdmin.Id && x.Login == currentAdmin.Login &&
                x.Password == currentAdmin.Password);
            if (currentAdmin != null && result != null)
            {
                var upd = _model.Employees.FirstOrDefault(x => x.Id == toUpdate.Id);
                if (upd == null)
                {
                    return false;
                }

                try
                {
                    upd.set(toUpdate);
                    _model.EmployeesChanges.Add(new EmployeeChange(currentAdmin, upd, "User data updated"));
                    _model.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    //_model.Entry(_model.Employees).CurrentValues.SetValues(_model.Entry(_model.Employees).OriginalValues);
                    //_model.Entry(_model.EmployeesChanges).CurrentValues.SetValues(_model.Entry(_model.EmployeesChanges).OriginalValues);
                    throw;
                }
            }
            else return false;
        }

        public List<Delivery> GetDeliveries()
        {
            return _model.Deliveries.ToList();
        }

        public bool AddDelivery(Delivery toAdd, Employee currentAdmin)
        {
            if (currentAdmin != null && _model.Employees.FirstOrDefault(x =>
                    x.Id == currentAdmin.Id && x.Login == currentAdmin.Login && x.Password == currentAdmin.Password)
                != null)
            {
                try
                {
                    toAdd.Employee = null;
                    var temp1 = toAdd.ProductDeliveries.Select(x => new ProductDelivery(x)).ToList();
                    toAdd.ProductDeliveries = null;
                    _model.Deliveries.Add(toAdd);
                    _model.DeliveriesChanges.Add(new DeliveriesChanges(currentAdmin, toAdd, "Delivery added"));
                    _model.SaveChanges();
                    foreach (var elem in temp1)
                    {
                        elem.Delivery = toAdd.Id;
                        elem.Delivery1 = null;
                        elem.Product = elem.Product1.Id;
                        elem.Product1 = null;
                        _model.ProductDeliveries.Add(new ProductDelivery(elem));
                    }
                    _model.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _model.Entry(_model.DeliveriesChanges).CurrentValues.SetValues(_model.Entry(_model.DeliveriesChanges).OriginalValues);
                    _model.Entry(_model.Deliveries).CurrentValues.SetValues(_model.Entry(_model.Deliveries).OriginalValues);
                    _model.Entry(_model.ProductDeliveries).CurrentValues.SetValues(_model.Entry(_model.ProductDeliveries).OriginalValues);
                    throw;
                }
            }

            return false;
        }

        public bool UpdateDelivery(Delivery toUpdate, Employee currentAdmin)
        {
            if (currentAdmin != null && _model.Employees.Where(x =>
                    x.Id == currentAdmin.Id && x.Login == currentAdmin.Login && x.Password == currentAdmin.Password)
                != null)
            {
                try
                {
                    var toUpd = _model.Deliveries.FirstOrDefault(x => x.Id == toUpdate.Id);
                    if (toUpd == null) return false;
                    toUpd.set(toUpdate);
                    foreach (var elem in toUpdate.ProductDeliveries)
                    {
                        elem.Delivery1 = toUpdate;
                        elem.Delivery = toUpdate.Id;
                        var dp = _model.ProductDeliveries.FirstOrDefault(x => x.Delivery == elem.Delivery && x.Product == elem.Product);
                        if (dp == null)
                        {
                            _model.ProductDeliveries.Add(elem);
                        }
                        else
                        {
                            dp.Set(elem);
                        }
                    }
                    _model.DeliveriesChanges.Add(new DeliveriesChanges(currentAdmin, toUpdate, "Delivery updated"));
                    _model.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _model.Entry(_model.DeliveriesChanges).CurrentValues.SetValues(_model.Entry(_model.DeliveriesChanges).OriginalValues);
                    _model.Entry(_model.Deliveries).CurrentValues.SetValues(_model.Entry(_model.Deliveries).OriginalValues);
                    throw;
                }
            }

            return false;
        }

        public List<Group> GetGroups()
        {
            return _model.Groups.ToList().Select(x => new Group(x)).ToList();
        }

        public void AddGroup(Group toAdd)
        {
            try
            {
                _model.Groups.Add(toAdd);
                _model.SaveChanges();
            }
            catch (Exception e)
            {
                _model.Entry(_model.Groups).CurrentValues.SetValues(_model.Entry(_model.Groups).OriginalValues);
                throw;
            }
        }

        public bool UpdateGroup(Group toUpdate)
        {
            var toUpd = _model.Groups.FirstOrDefault(x => x.Id == toUpdate.Id);
            if (toUpd != null)
            {
                try
                {
                    toUpd.set(toUpdate);
                    _model.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    _model.Entry(_model.Groups).CurrentValues.SetValues(_model.Entry(_model.Groups).OriginalValues);
                    throw;
                }
            }

            return false;
        }

        public List<Product> GetProducts(Func<Product, bool> predicate)
        {
            return _model.Products.Where(predicate).ToList().Select(x => new Product(x)).ToList();
        }

        public bool AddProduct(Product toAdd, Employee currentAdmin)
        {

            if (currentAdmin != null && _model.Employees.FirstOrDefault(x =>
                    x.Id == currentAdmin.Id && x.Login == currentAdmin.Login && x.Password == currentAdmin.Password)
                != null)
            {
                try
                {
                    _model.Products.Add(toAdd);
                    _model.ProductsChanges.Add(new ProductChange(currentAdmin, toAdd, "Product added"));
                    _model.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _model.Entry(_model.Products).CurrentValues.SetValues(_model.Entry(_model.Products).OriginalValues);
                    _model.Entry(_model.ProductsChanges).CurrentValues.SetValues(_model.Entry(_model.ProductsChanges).OriginalValues);
                    throw;
                }
            }

            return false;
        }

        public bool UpdateProduct(Product toAdd, Employee currentAdmin)
        {
            if (currentAdmin != null && _model.Employees.FirstOrDefault(x =>
                    x.Id == currentAdmin.Id && x.Login == currentAdmin.Login && x.Password == currentAdmin.Password)
                != null)
            {
                try
                {
                    var toUpd = _model.Products.FirstOrDefault(x => x.Id == toAdd.Id);
                    if (toUpd == null) return false;
                    toUpd.set(toAdd);
                    _model.ProductsChanges.Add(new ProductChange(currentAdmin, toAdd, "Product changed"));
                    _model.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    _model.Entry(_model.Products).CurrentValues.SetValues(_model.Entry(_model.Products).OriginalValues);
                    _model.Entry(_model.ProductsChanges).CurrentValues.SetValues(_model.Entry(_model.ProductsChanges).OriginalValues);
                    throw;
                }
            }

            return false;
        }

        public List<Sale> GetSales(Func<Sale, bool> predicate)
        {
            return _model.Sales.Where(predicate).ToList();
        }

        public bool AddSale(Sale toAdd, Employee currentCashier)
        {
            if (currentCashier != null && _model.Employees.FirstOrDefault(x =>
                    x.Id == currentCashier.Id && x.Login == currentCashier.Login &&
                           x.Password == currentCashier.Password)
                != null)
            {
                try
                {
                    toAdd.Employee = currentCashier;
                    toAdd.Seller_Id = currentCashier.Id;
                    toAdd.Date = DateTime.Now;
                    var ttemp = new List<SaleProduct>(toAdd.SaleProducts.Select(x => new SaleProduct(x)));
                    toAdd.SaleProducts = new List<SaleProduct>();
                    _model.Sales.Add(toAdd);
                    _model.SaveChanges();
                    //_model.SaveChanges();
                    foreach (var elem in ttemp)
                    {
                        elem.Sale = toAdd.Id;
                        elem.Sale1 = toAdd;
                        _model.SaleProducts.Add(elem);
                    }

                    _model.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    _model.Entry(_model.Sales).CurrentValues.SetValues(_model.Entry(_model.Sales).OriginalValues);
                    _model.Entry(_model.SaleProducts).CurrentValues.SetValues(_model.Entry(_model.SaleProducts).OriginalValues);
                    throw;
                }
            }

            return false;
        }


        public List<Supplier> GetSuppliers()
        {
            return _model.Suppliers.ToList().Select(x => new Supplier(x)).ToList();
        }

        public void AddSupplier(Supplier toAdd)
        {
            try
            {
                _model.Suppliers.Add(toAdd);
                _model.SaveChanges();
            }
            catch (Exception e)
            {
                _model.Entry(_model.Sales).CurrentValues.SetValues(_model.Entry(_model.Sales).OriginalValues);
                throw;
            }


        }

        public bool UpdateSupplier(Supplier toAdd)
        {

            try
            {
                var toUpdate = _model.Suppliers.FirstOrDefault(x => x.Id == toAdd.Id);
                if (toUpdate == null)
                    return false;
                toUpdate.set(toAdd);
                _model.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _model.Entry(_model.Sales).CurrentValues.SetValues(_model.Entry(_model.Sales).OriginalValues);
                throw;
            }

        }

        // public List<Change> GetChanges(Func<Change, bool> predicate)
        // {
        //     throw new NotImplementedException();
        // }
        public void Dispose()
        {
            _model?.Dispose();
        }

    }


}
