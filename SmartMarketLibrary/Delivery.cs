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
        }

        public Delivery(Delivery toCopy)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
