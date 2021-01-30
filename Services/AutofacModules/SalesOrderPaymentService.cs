using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Data.Models;
using Services.Interfaces;
using Services.Repository;
using Services.VM;

namespace Services.AutofacModules
{
    public class SalesOrderPaymentService : ISalesOrderPaymentService
    {
        private UnitOfWork unitOfWork;
        private ICustomerService customerService;
        private IProductService productService;
        private IChequeService chequeService;

        public SalesOrderPaymentService(UnitOfWork unitOfWork, ICustomerService customerService, IProductService productService,
            IChequeService chequeService
        )
        {
            this.unitOfWork = unitOfWork;
            this.customerService = customerService;
            this.productService = productService;
            this.chequeService = chequeService;
        }


        #region Unsettled Payment Service
        public List<SalesOrders> GetUnsettledPayments()
        {
            return unitOfWork.SalesOrderRepo.Get(x => x.Balance > 0);
        }
        public SalesOrderPayments GetDetails(int? paymentSalesOrderId)
        {
            return unitOfWork.SalesOrderPaymentRepo.Find(x => x.Id == paymentSalesOrderId);
        }
        public List<Customers> GetCustomers()
        {
            return customerService.Customers();
        }

        public List<Products> GetProducts()
        {
            return productService.Products();
        }
        public List<Cheques> GetCheques()
        {
            return unitOfWork.ChequesRepo.Get(x => !x.SalesOrderPayments.Any(), includeProperties: "SalesOrderPayments");
        }

        public Results Insert(SalesOrderPaymentsVM model)
        {
            try
            {
                var cheque = unitOfWork.ChequesRepo.Find(x => x.Id == model.ChequeId);
                var chequeBalance = cheque?.Amount;

                foreach (var i in unitOfWork.SalesOrderRepo.Get(x => x.CustomerId == model.CustomerId && x.Balance > 0))
                {

                    decimal? amountPaid = 0;
                    if (chequeBalance > i.Balance)
                    {
                        chequeBalance -= i.Balance;
                        amountPaid = i.Balance;
                    }
                    else
                    {

                        amountPaid = chequeBalance;
                        chequeBalance = 0;
                    }

                    unitOfWork.SalesOrderPaymentRepo.Insert(new SalesOrderPayments()
                    {
                        ChequeId = model.ChequeId,
                        Amount = amountPaid,
                        SalesOrderId = i.Id,
                        DateCreated = DateTime.Now

                    });

                    i.AmountPaid += amountPaid;



                    unitOfWork.SalesOrderPaymentRepo.SaveChanges();
                    UpdateSalesOrder(i);

                }
                return new Results()
                {
                    Succeeded = true
                };
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return new Results()
                {
                    Succeeded = false,
                    Errors = new List<string>() { e.Message }
                };
            }
        }

        public void UpdateSalesOrder(SalesOrders model)
        {
            var vm = unitOfWork.SalesOrderPaymentRepo.Fetch(x => x.SalesOrderId == model.Id).Sum(x => x.Amount);
            if (model.TotalAmount >= vm)
                model.Status = "Tendered Transaction";
            unitOfWork.SalesOrderRepo.Update(model);
            unitOfWork.SalesOrderRepo.SaveChanges();
        }



        #endregion

        #region Payment History Service
        public List<SalesOrderPayments> GetPaymentHistory()
        {
            return unitOfWork.SalesOrderPaymentRepo.Get().Select(x => new SalesOrderPayments()
            {
                Amount = x.Amount,
                DateCreated = x.DateCreated,
                SalesOrders = unitOfWork.SalesOrderRepo.Find(m => m.Id == x.SalesOrderId),
                Cheque = unitOfWork.ChequesRepo.Find(m => m.Id == x.ChequeId),
                ChequeId = x.ChequeId,
                Id = x.Id,
                SalesOrderId = x.SalesOrderId
            }).ToList();
        }


        #endregion
    }
}
