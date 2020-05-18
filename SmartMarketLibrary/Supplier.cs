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
            _changes = toCopy._changes;
            Id = toCopy.Id;
            Name = toCopy.Name;
            NIP = toCopy.NIP;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Supplier supplier)
            {
                return ((_changes?.Equals(supplier._changes) ?? supplier._changes == null) && Id == supplier.Id &&
                       Name == supplier.Name && NIP == supplier.NIP);
            }

            return false;
        }
    }
}
