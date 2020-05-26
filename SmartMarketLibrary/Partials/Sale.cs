using System.Linq;

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
            SaleProducts = toCopy.SaleProducts;
        }
        public decimal SumValue
        {
            get => SaleProducts.Select(x => x.Price * x.Quantity).Sum();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Sale sale)
            {
                return sale.Id == Id && sale.Date == Date && sale.Seller_Id == Seller_Id &&
                       (sale.Employee?.Equals(Employee) ?? Employee == null) && (sale.SaleProducts?.Equals(SaleProducts) ?? SaleProducts == null);
            }

            return false;
        }
    }
}
