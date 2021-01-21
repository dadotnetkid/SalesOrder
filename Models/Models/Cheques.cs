using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Cheques
    {
        public Cheques()
        {
            ChequeInSalesOrder = new HashSet<ChequeInSalesOrder>();
        }

        public string Id { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string Payee { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string ChequeNumber { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankBranch { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ICollection<ChequeInSalesOrder> ChequeInSalesOrder { get; set; }
    }
}
