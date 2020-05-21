﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    partial class Group
    {
        public Group(Group toCopy)
        {
            Id = toCopy.Id;
            Name = toCopy.Name;
            Vat = toCopy.Vat;
            Is18 = toCopy.Is18;
            Products = toCopy.Products;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Group group)
            {
                return Id == group.Id && Name == group.Name && Vat == group.Vat && Is18 == group.Is18;
            }

            return false;
        }

        public void set(Group toCopy)
        {
            Id = toCopy.Id;
            Name = toCopy.Name;
            Vat = toCopy.Vat;
            Is18 = toCopy.Is18;
        }
    }
}
