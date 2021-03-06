﻿namespace SmartMarketLibrary
{
    partial class Supplier
    {
        public override string ToString()
        {
            return Name;
        }

        public Supplier(Supplier toCopy)
        {
            Id = toCopy.Id;
            Name = toCopy.Name;
            NIP = toCopy.NIP;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Supplier supplier)
            {
                return Id == supplier.Id && Name == supplier.Name && NIP == supplier.NIP;
            }

            return false;
        }

        public void set(Supplier toCopy)
        {
            Name = toCopy.Name;
            NIP = toCopy.NIP;
        }
    }
}
