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
            Id = toCopy.Id;
            Seller = toCopy.Seller;
            Sold = toCopy.Sold;
            Date = toCopy.Date;
        }
        
        public override bool Equals(object obj)
        {
            if (obj==null)
            {
                return false;
            }

            if (obj is Sale sale)
            {
                return Id == sale.Id && Seller == sale.Seller && Sold == sale.Sold && Date == sale.Date;
            }

            return false;
        }
    }
}
