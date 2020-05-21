using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    partial class Delivery
    {
        public Delivery(Delivery toCopy)
        {
            Id = toCopy.Id;
            Invoice = toCopy.Invoice;
            Date = toCopy.Date;
            Manager_Id = toCopy.Manager_Id;
            DeliveriesChanges = toCopy.DeliveriesChanges;
            Employee = toCopy.Employee;
            Products = toCopy.Products;
            Supplier = toCopy.Supplier;
            SupplierId = toCopy.SupplierId;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Delivery delivery)
            {
                return Id == delivery.Id && Invoice == delivery.Invoice && Date == delivery.Date &&
                       Manager_Id == delivery.Manager_Id &&
                       (Employee?.Equals(delivery.Employee) ?? delivery.Employee == null) &&
                       (Products?.Equals(delivery.Products) ?? delivery.Products == null);
            }

            return false;
        }

        public void set(Delivery toCopy)
        {
            Invoice = toCopy.Invoice;
            Date = toCopy.Date;
            Products = toCopy.Products;
            Supplier = toCopy.Supplier;
            SupplierId = toCopy.SupplierId;
        }
    }
}
