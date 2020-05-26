using System;

namespace SmartMarketLibrary
{
    partial class DeliveriesChanges
    {
        public DeliveriesChanges()
        {
        }

        public DeliveriesChanges(Employee changing, Delivery changed, string detalis)
        {
            Date = DateTime.Now;
            Details = detalis;
            Changing_Id = changing.Id;
            Changed_Id = changed.Id;
            // Employee = changing;
            // Delivery = changed;
        }

        public DeliveriesChanges(DeliveriesChanges toCopy)
        {
            Id = toCopy.Id;
            Date = toCopy.Date;
            Details = toCopy.Details;
            Changing_Id = toCopy.Changing_Id;
            Changed_Id = toCopy.Changed_Id;
            Delivery = toCopy.Delivery;
            Employee = toCopy.Employee;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is DeliveriesChanges change)
            {
                return Id == change.Id && Date == change.Date && Details == change.Details &&
                       Changing_Id == change.Changing_Id && Changed_Id == change.Changed_Id;
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
            Changed_Id = changed.Id;
            // Employee = changing;
            // Employee1 = changed;
        }

        public EmployeeChange(EmployeeChange toCopy)
        {
            Id = toCopy.Id;
            Date = toCopy.Date;
            Details = toCopy.Details;
            Changing_Id = toCopy.Changing_Id;
            Changed_Id = toCopy.Changed_Id;
            Employee1 = toCopy.Employee1;
            Employee = toCopy.Employee;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is EmployeeChange change)
            {
                return Id == change.Id && Date == change.Date && Details == change.Details &&
                       Changing_Id == change.Changing_Id && Changed_Id == change.Changed_Id;
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
            Changed_Id = changed.Id;
        }

        public ProductChange(ProductChange toCopy)
        {
            Id = toCopy.Id;
            Date = toCopy.Date;
            Details = toCopy.Details;
            Changing_Id = toCopy.Changing_Id;
            Changed_Id = toCopy.Changed_Id;
            // Product = toCopy.Product;
            // Employee = toCopy.Employee;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is ProductChange change)
            {
                return Id == change.Id && Date == change.Date && Details == change.Details &&
                       Changing_Id == change.Changing_Id && Changed_Id == change.Changed_Id;
            }

            return false;
        }
    }

}
