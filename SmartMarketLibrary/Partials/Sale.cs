using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    partial class Sale
    {
        public Sale(Sale toCopy)
        {
            Id = toCopy.Id;
            Date = toCopy.Date;
            Seller_Id = toCopy.Seller_Id;
            Employee = toCopy.Employee;
            Products = toCopy.Products;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Sale sale)
            {
                return sale.Id == Id && sale.Date == Date && sale.Seller_Id == Seller_Id &&
                       (sale.Employee?.Equals(Employee)??Employee==null) && (sale.Products?.Equals(Products)??Products==null);
            }

            return false;
        }
    }
}
