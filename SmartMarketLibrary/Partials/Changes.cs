using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    partial class DeliveryChange
    {
        public DeliveryChange()
        {
        }

        public DeliveryChange(Employee changing, Delivery changed, string detalis)
        {
            Date=DateTime.Now;
            Details = detalis;
            Changing_Id = changing.Id;
            Changed_ID = changed.Id;
            Employee = changing;
            Delivery = changed;
        }

        public DeliveryChange(DeliveryChange toCopy)
        {
            Id = toCopy.Id;
            Date = toCopy.Date;
            Details = toCopy.Details;
            Changing_Id = toCopy.Changing_Id;
            Changed_ID = toCopy.Changed_ID;
            Delivery = toCopy.Delivery;
            Employee = toCopy.Employee;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is DeliveryChange change)
            {
                return Id == change.Id && Date == change.Date && Details == change.Details &&
                       Changing_Id == change.Changing_Id && Changed_ID == change.Changed_ID;
            }

            return false;
        }
    }

    partial class EmployeeChange
    {
        public EmployeeChange()
        {
        }

        public EmployeeChange(Employee changing, Employee changed, string detalis)
        {
            Date = DateTime.Now;
            Details = detalis;
            Changing_Id = changing.Id;
            Changed_ID = changed.Id;
            Employee = changing;
            Employee1 = changed;
        }

        public EmployeeChange(EmployeeChange toCopy)
        {
            Id = toCopy.Id;
            Date = toCopy.Date;
            Details = toCopy.Details;
            Changing_Id = toCopy.Changing_Id;
            Changed_ID = toCopy.Changed_ID;
            Employee1 = toCopy.Employee1;
            Employee = toCopy.Employee;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is EmployeeChange change)
            {
                return Id == change.Id && Date == change.Date && Details == change.Details &&
                       Changing_Id == change.Changing_Id && Changed_ID == change.Changed_ID;
            }

            return false;
        }
    }

    partial class ProductChange
    {
        public ProductChange()
        {
        }

        public ProductChange(Employee changing, Product changed, string detalis)
        {
            Date = DateTime.Now;
            Details = detalis;
            Changing_Id = changing.Id;
            Changed_ID = changed.Id;
            Employee = changing;
            Product = changed;
        }

        public ProductChange(ProductChange toCopy)
        {
            Id = toCopy.Id;
            Date = toCopy.Date;
            Details = toCopy.Details;
            Changing_Id = toCopy.Changing_Id;
            Changed_ID = toCopy.Changed_ID;
            Product = toCopy.Product;
            Employee = toCopy.Employee;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is ProductChange change)
            {
                return Id == change.Id && Date == change.Date && Details == change.Details &&
                       Changing_Id == change.Changing_Id && Changed_ID == change.Changed_ID;
            }

            return false;
        }
    }
    
}
