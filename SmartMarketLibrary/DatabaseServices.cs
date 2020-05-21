using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;

namespace SmartMarketLibrary
{
    public class DatabaseServices
    {
        private Entities _model;

        public DatabaseServices(Entities model)
        {
            _model = model;
        }

        public Employee Login(string login, string password)
        {
            Employee toRet = null;
            var passwd = GetMd5Hash(password);
            toRet = _model.Employees.FirstOrDefault(x => x.Login == login && x.Password == passwd);
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
            return _model.Employees.ToList();
        }

        public bool AddEmployee(Employee toAdd, Employee currentAdmin)
        {

            if (currentAdmin != null &&
                !_model.Employees.Where(x =>
                        x.Id == currentAdmin.Id && x.Login == currentAdmin.Login && x.Password == currentAdmin.Password)
                    .IsNullOrEmpty() && _model.Employees.Where(x => x.Login == toAdd.Login).IsNullOrEmpty())
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
            if (currentAdmin != null && !_model.Employees.Where(x => x.Id == currentAdmin.Id && x.Login == currentAdmin.Login && x.Password == currentAdmin.Password).IsNullOrEmpty() && !_model.Employees.Where(x => x.Id == currentAdmin.Id && x.Login == currentAdmin.Login && x.Password == currentAdmin.Password).IsNullOrEmpty())
            {
                var upd = _model.Employees.FirstOrDefault(x => x.Id == toUpdate.Id);
                if (upd == null)
                {
                    return false;
                }

                try
                {
                    upd.set(toUpdate);
                    _model.EmployeesChanges.Add(new EmployeeChange(currentAdmin, toUpdate, "User data updated"));
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
            else return false;
        }

        public List<Delivery> GetDeliveries()
        {
            return _model.Deliveries.ToList();
        }

        public bool AddDelivery(Delivery toAdd, Employee currentAdmin)
        {
            if (currentAdmin != null && !_model.Employees.Where(x =>
                    x.Id == currentAdmin.Id && x.Login == currentAdmin.Login && x.Password == currentAdmin.Password)
                .IsNullOrEmpty())
            {
                try
                {
                    _model.Deliveries.Add(toAdd);
                    _model.DeliveriesChanges.Add(new DeliveryChange(currentAdmin, toAdd, "Delivery added"));
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

        public bool UpdateDelivery(Delivery toUpdate, Employee currentAdmin)
        {
            if (currentAdmin != null && !_model.Employees.Where(x =>
                    x.Id == currentAdmin.Id && x.Login == currentAdmin.Login && x.Password == currentAdmin.Password)
                .IsNullOrEmpty())
            {
                try
                {
                    var toUpd = _model.Deliveries.FirstOrDefault(x => x.Id == toUpdate.Id);
                    if (toUpd == null) return false;
                    toUpd.set(toUpdate);
                    _model.DeliveriesChanges.Add(new DeliveryChange(currentAdmin, toUpdate, "Delivery updated"));
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
            return _model.Groups.ToList();
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
            return _model.Products.Where(predicate).ToList();
        }

        public int AddProduct(Product toAdd, Employee currentAdmin)
        {
            throw new NotImplementedException();
        }

        public int UpdateProduct(Product toAdd, Employee currentAdmin)
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetSales(Func<Sale, bool> predicate)
        {
            return _model.Sales.Where(predicate).ToList();
        }

        public void AddSale(Sale toAdd, Employee currentCashier)
        {
            throw new NotImplementedException();
        }


        public List<Supplier> GetSuppliers()
        {
            return _model.Suppliers.ToList();
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
    }


}
