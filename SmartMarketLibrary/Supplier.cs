using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    public class Supplier
    {
        private List<Change> _changes;
        public int Id { get; set; }
        public string Name { get; set; }
        public string NIP { get; set; }
        public IReadOnlyList<Change> Changes { get=>_changes; }

        public Supplier()
        {
        }

        public Supplier(Supplier toCopy)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
