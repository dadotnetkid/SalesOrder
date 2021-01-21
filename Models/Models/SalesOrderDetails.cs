using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class SalesOrderDetails
    {
        public string Id { get; set; }
        public string SalesOrderId { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public decimal? PurchaseAmount { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? Qty { get; set; }
        public decimal? SubTotal { get; set; }

        public virtual SalesOrders SalesOrder { get; set; }
    }
}
