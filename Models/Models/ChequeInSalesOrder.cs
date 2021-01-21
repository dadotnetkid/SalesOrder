using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class ChequeInSalesOrder
    {
        public string ChequeId { get; set; }
        public string SalesOrderId { get; set; }

        public virtual Cheques Cheque { get; set; }
        public virtual SalesOrders SalesOrder { get; set; }
    }
}
