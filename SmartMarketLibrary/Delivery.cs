using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    public class Delivery
    {
        public int Id { get; set; }
        private List<Change> _changes;

        public Employee Manager { get; set; }
        public string Invoice { get; set; }
        public List<Tuple<Product, int, decimal>> Products { get; set; }
        public DateTime Date { get; set; }
        public IReadOnlyList<Change> Changes { get=>_changes; }

        public Delivery()
        {
            _changes=new List<Change>();
        }

        public Delivery(Delivery toCopy)
        {
            Id = toCopy.Id;
            _changes = toCopy._changes;
            Manager = toCopy.Manager;
            Products = toCopy.Products;
            Date = toCopy.Date;
            Invoice = toCopy.Invoice;
        }

        public override bool Equals(object obj)
        {
            if (obj is Delivery delivery)
            {
                return Id == delivery.Id && Manager.Equals(delivery.Manager) && _changes.Equals(delivery.Changes) &&
                       Invoice == delivery.Invoice && Products.Equals(delivery.Products) && Date.Equals(delivery.Date);
            }

            return false;
        }
    }
}
