using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    public class DatabaseServices
    {
        private DBModel _model;

        public DatabaseServices(DBModel model)
        {
            _model = model;
        }

        public Employee Login(string login, string password)
        {
            throw new NotImplementedException();
        }

        internal static string GetMd5Hash(string input)
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
            throw new NotImplementedException();
        }

        public bool AddEmployee(Employee toAdd, Employee currentAdmin)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmployee(Employee toUpdate, Employee currentAdmin)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetDeliveries()
        {
            throw new NotImplementedException();
        }

        public void AddDelivery(Delivery toAdd, Employee currentAdmin)
        {
            throw new NotImplementedException();
        }

        public int UpdateDelivery(Delivery toAdd, Employee currentAdmin)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetGroups()
        {
            throw new NotImplementedException();
        }

        public void AddGroup(Group toAdd)
        {
            throw new NotImplementedException();
        }

        public int UpdateGroup(Group toAdd)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void AddSale(Sale toAdd, Employee currentCashier)
        {
            throw new NotImplementedException();
        }


        public List<Supplier> GetSuppliers()
        {
            throw new NotImplementedException();
        }

        public void AddSupplier(Supplier toAdd, Employee currentAdmin)
        {
            throw new NotImplementedException();
        }

        public int UpdateSupplier(Supplier toAdd, Employee currentAdmin)
        {
            throw new NotImplementedException();
        }

        public List<Change> GetChanges(Func<Change, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
