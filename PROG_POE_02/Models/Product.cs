//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PROG_POE_02.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int Product_id { get; set; }
        public Nullable<int> id { get; set; }
        public string Product_name { get; set; }
        public string Product_Description { get; set; }
        public string Product_Type { get; set; }
    
        public virtual Ulogin Ulogin { get; set; }
    }
}
