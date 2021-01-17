using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public partial class SalesOrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int? SalesOrderId { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public decimal? PurchaseAmount { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? Qty { get; set; }
        public decimal? SubTotal { get; set; }

        public virtual SalesOrders SalesOrder { get; set; }
    }
}
