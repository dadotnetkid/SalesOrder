using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Products
    {
        public string Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public decimal? PurchaseAmount { get; set; }
        public decimal? SellingAmount { get; set; }
        public int ToleranceLevel { get; set; }
        public bool CanExceed { get; set; }
    }
}
