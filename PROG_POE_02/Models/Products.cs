using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG_POE_02.Models
{
    public partial class Products
    {
        public  string Product_Name { get; set; }
        public  string Product_Description { get; set; }
        public  string Product_Type { get; set; }
        public string farmer_id { get; set; }
        public string Product_id { get; set; }

    }
}