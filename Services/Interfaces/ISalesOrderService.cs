using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Services.VM;

namespace Services.Interfaces
{
    public interface ISalesOrderService
    {
        public SalesOrders Find(Func<SalesOrders, bool> filter = null);
        public List<SalesOrders> Orders(Func<SalesOrders, bool> filter = null);
        public SalesOrders Insert(SalesOrders salesOrders);
        public SalesOrders Update(SalesOrders salesOrders);
        public SalesOrders Update(SalesOrderVM vm) ;
        public int Delete(string salesOrder);
        public SalesOrders Initialize();
        public void TenderTransaction(int? saleOrderId);
        public void TenderTransaction(SalesOrders salesOrders);
        public void CancelTransaction(int? saleOrderId);
    }
}
