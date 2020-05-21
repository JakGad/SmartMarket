using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    partial class Product
    {
        public Product(Product toCopy)
        {
            Id = toCopy.Id;
            Name = toCopy.Name;
            Manufacturer = toCopy.Manufacturer;
            NetPrice = toCopy.NetPrice;
            Quantity = toCopy.Quantity;
            Code = toCopy.Code;
            Margin = toCopy.Margin;
            Availability = toCopy.Availability;
            ProductGroup_Id = toCopy.ProductGroup_Id;
            Group = toCopy.Group;
            ProductsChanges = toCopy.ProductsChanges;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Product product)
            {
                return Id == product.Id && Name == product.Name && Manufacturer == product.Manufacturer &&
                       NetPrice == product.NetPrice && Quantity == product.Quantity && Code == product.Code &&
                       Margin == product.Margin && Availability == product.Availability &&
                       ProductGroup_Id == product.ProductGroup_Id;
            }

            return false;
        }
        public decimal Price
        {
            get => NetPrice + Margin * NetPrice + (Group?.Vat ?? 0) * NetPrice;
        }
    }
}
