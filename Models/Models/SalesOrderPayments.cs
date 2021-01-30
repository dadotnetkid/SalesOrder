using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("SalesOrderPayments")]
    public class SalesOrderPayments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(128)]
        public string SalesOrderId { get; set; }
        [MaxLength(128)]
        public string ChequeId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DateCreated { get; set; }
        public Cheques Cheque { get; set; }
        public SalesOrders SalesOrders { get; set; }
    }
}
