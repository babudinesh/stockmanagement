using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockManagementDemo.Models
{
    public class Stock
    {
       
            [Key]
            public int StockID { get; set; }
            public string Fruit { get; set; }
            public string Variety { get; set; }
            public int Quantity { get; set; }
    }
}