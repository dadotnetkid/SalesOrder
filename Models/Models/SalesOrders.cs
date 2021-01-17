using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class SalesOrders
    {
        public SalesOrders()
        {
            SalesOrderDetails = new HashSet<SalesOrderDetails>();
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public decimal? AmountPaid { get; set; }

        public virtual AspNetUsers CreatedByNavigation { get; set; }
        public virtual ICollection<SalesOrderDetails> SalesOrderDetails { get; set; }
    }
}
