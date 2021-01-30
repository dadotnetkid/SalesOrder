using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public partial class Cheques
    {
        public Cheques()
        {

            this.SalesOrderPayments = new HashSet<SalesOrderPayments>();
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
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ChequeNumberAndAmount => this.ChequeNumber + " - " + this.Amount?.ToString("n2");

        public virtual ICollection<SalesOrderPayments> SalesOrderPayments { get; set; }
    }
}
