using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    public class Product:IProduct, IManagerProduct
    {
        private List<Change> _changes;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal NetPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Price
        {
            get => NetPrice + NetPrice * Margin + NetPrice * ProductGroup.Vat;
        }
        public string Code { get; set; }
        public decimal Margin { get; set; }
        public bool Availability { get; set; }
        public Group ProductGroup { get; set; }
        public IReadOnlyList<Change> Changes { get=>_changes; }

        public Product()
        {
        }

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
            ProductGroup = toCopy.ProductGroup;
            _changes = toCopy._changes;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Product product)
            {
                return Id == product.Id && Name == product.Name && Manufacturer == product.Manufacturer &&
                       NetPrice == product.NetPrice &&
                       Quantity == product.Quantity && Code == product.Code && Margin == product.Margin &&
                       Availability == product.Availability &&
                       ProductGroup.Equals(product.ProductGroup) && (_changes?.Equals(product._changes) ?? product._changes == null);
            }

            return false;
        }
    }
}
