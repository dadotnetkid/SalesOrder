using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Services.Repository;
using Services.VM;

namespace Services.Interfaces
{
    public interface ISalesOrderPaymentService 
    {
        List<SalesOrders> GetUnsettledPayments();

        SalesOrderPayments GetDetails(int? paymentSalesOrderId);
        List<Customers> GetCustomers();
        List<Products> GetProducts();
        List<Cheques> GetCheques();
        Results Insert(SalesOrderPaymentsVM model);
        void UpdateSalesOrder(SalesOrders model);
        List<SalesOrderPayments> GetPaymentHistory();
    }
}
