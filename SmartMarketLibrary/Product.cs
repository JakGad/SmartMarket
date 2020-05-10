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
        public decimal Price { get; }
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
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
