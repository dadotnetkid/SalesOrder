using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Services.Interfaces;

namespace Services.VM
{
    public class SalesOrderVM
    {
        public string saleOrderId
        {
            get => _saleOrderId;
            set => _saleOrderId = value;
        }

        private SalesOrders salesOrder;
        private ICustomerService customerService;
        public bool isSuccess;
        private List<Cheques> _cheques;
        private string _saleOrderId;

        public SalesOrderVM()
        {
            customerService = new CustomerService();

        }

        public List<Customers> Customers => customerService.Customers();
        public SalesOrders SalesOrder
        {
            get
            {
                if (salesOrder == null)
                    salesOrder = new SalesOrders();
                return salesOrder;
            }
            set => salesOrder = value;
        }

        public List<string> PaymentMethods => new List<string>() { "Cash", "Cheque" };

        public List<Cheques> Cheques
        {
            get
            {
                if (_cheques == null)
                    _cheques = new List<Cheques>();
                    return _cheques;
            }
            set => _cheques = value;
        }

        public string ChequeId { get; set; }
    }
}
