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
            Id = toCopy.Id;
            Name = toCopy.Name;
            Vat = toCopy.Vat;
            Is18 = toCopy.Is18;
        }

        public override bool Equals(object obj)
        {
            if (obj is Group group)
            {
                return Id == group.Id && Name == group.Name && Vat == group.Vat && Is18 == group.Is18;
            }

            return false;
        }
    }
}
