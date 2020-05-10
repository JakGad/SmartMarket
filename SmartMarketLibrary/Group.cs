using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Vat { get; set; }
        public bool Is18 { get; set; }

        public Group()
        {
        }

        public Group(Group toCopy)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
