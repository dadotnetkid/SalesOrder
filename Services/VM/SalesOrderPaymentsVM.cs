using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Services.Interfaces;
using Services.Repository;

namespace Services.VM
{
    public class SalesOrderPaymentsVM
    {
        
        public List<Cheques> Cheques { get; set; }

        public List<Customers> Customers { get; set; }


        public string CustomerId { get; set; }
        public string ChequeId { get; set; }
        public string SalesOrderId { get; set; }
        public SalesOrderPayments SalesOrderPayments { get; set; }
        public int PaymentSalesOrderId { get; set; }
    }
}
