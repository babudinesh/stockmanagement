using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace StockManagementDemo.Models
{
    public class BatchModel
    {
        [Key]
        public int BatchID { get; set; }
        public string Fruit { get; set; }
        public string Variety { get; set; }
        public int Quantity { get; set; }

    }

    public class BatchDBContext : DbContext
    {
        public DbSet<BatchModel> BatchModels { get; set; }
        public DbSet<Stock> stocks { get; set; }
    }

   
}