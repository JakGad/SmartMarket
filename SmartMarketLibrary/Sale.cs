using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    public class Sale:ISale, ICashierSale
    {
        public int Id { get; set; }
        public Employee Seller { get; set; }
        public IReadOnlyList<Tuple<Product, int, decimal>> Sold { get; set; }
        public DateTime Date { get; set; }

        public Sale()
        {

        }

        public Sale(Sale toCopy)
        {
            throw new NotImplementedException();
        }
        
        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
