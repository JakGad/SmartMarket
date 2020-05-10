using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
        public Change(){}

        public Change(Change toCopy)
        {
            throw new NotImplementedException();
        }
        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
