//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartMarketLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductChange
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string Details { get; set; }
        public Nullable<int> Changing_Id { get; set; }
        public Nullable<int> Changed_ID { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Product Product { get; set; }
    }
}
