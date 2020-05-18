using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketLibrary
{
    public class Change
    {
        public int Id { get; set; }
        public Employee Changing { get;  set; }
        public DateTime Date { get;  set; }
        public string Details { get;  set; }
        public Change(Employee changing, string details)
        {
            Changing = changing;
            Date=DateTime.Now;
            Details = details;
        }
        public Change(){}

        public Change(Change toCopy)
        {
            Id = toCopy.Id;
            Changing = toCopy.Changing;
            Date = toCopy.Date;
            Details = toCopy.Details;
        }
        public override bool Equals(object obj)
        {
            if (obj is Change change)
            {
                return Id == change.Id && Changing == change.Changing && Date == change.Date && Details == change.Details;
            }
            else
            {
                return false;
            }
        }
    }
}
