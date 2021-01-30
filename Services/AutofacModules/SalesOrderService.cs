using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using Services.Interfaces;
using Services.Repository;
using Services.VM;

namespace Services.AutofacModules
{
    public class SalesOrderService : ISalesOrderService
    {
        private ModelDb db;
        private IHttpContextAccessor httpContextAccessor;
        private ICustomerService customerService;
        private UnitOfWork unitOfWork;

        public SalesOrderService(ModelDb db, UnitOfWork unitOfWork, ICustomerService customerService, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.customerService = customerService;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }
        public SalesOrders Find(Func<SalesOrders, bool> filter = null)
        {
            if (filter != null)
                return db.SalesOrders.Include(x => x.SalesOrderDetails).FirstOrDefault(filter);
            return db.SalesOrders.Include(x => x.SalesOrderDetails).FirstOrDefault();
        }

        public List<SalesOrders> Get(Func<SalesOrders, bool> filter = null)
        {
            if (filter != null)
                return db.SalesOrders.Include(x => x.SalesOrderDetails).Where(filter).ToList();
            return db.SalesOrders.Include(x => x.SalesOrderDetails).ToList();
        }


        public SalesOrders Insert(SalesOrders salesOrders)
        {
            try
            {
                db.SalesOrders.Add(salesOrders);
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return salesOrders;
        }

        public SalesOrders Update(SalesOrders salesOrders)
        {
            try
            {
                var item = db.SalesOrders.Find(salesOrders);
                item.CustomerName = salesOrders.CustomerName;
                item.AmountPaid = salesOrders.AmountPaid;

                db.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return salesOrders;
        }

        public SalesOrders Update(SalesOrderVM vm)
        {
            try
            {
                var item = unitOfWork.SalesOrderRepo.Find(x => x.Id == vm.saleOrderId, "SalesOrderPayments,SalesOrderDetails");

                var customer = customerService.Customers().FirstOrDefault(x => x.Id == vm.SalesOrder.CustomerId);

                //get total amount
                var totalAmount = item.SalesOrderDetails.Sum(x => x.SubTotal) ?? 0;


                item.CustomerId = customer.Id;
                item.CustomerName = customer.FullName;
                item.AmountPaid = vm.SalesOrder.AmountPaid ?? 0;
                item.Status = "Tendered Transaction";
                //first is to clear just to be safe if cheque is already added
                unitOfWork.SalesOrderPaymentRepo.Delete(x => x.SalesOrderId == item.Id);

                foreach (var payment in item.SalesOrderPayments)
                {
                    unitOfWork.SalesOrderPaymentRepo.Insert(new SalesOrderPayments()
                    {
                        ChequeId = payment.ChequeId,
                        Amount = payment.Amount,
                        DateCreated = payment.DateCreated,
                        SalesOrderId = payment.SalesOrderId
                    });
                }

                unitOfWork.SalesOrderPaymentRepo.Insert(new SalesOrderPayments()
                {
                    ChequeId = vm.ChequeId,
                    Amount = item.AmountPaid,
                    DateCreated = DateTime.Now,
                    SalesOrderId = item.Id,

                });
                if (!string.IsNullOrEmpty(vm.ChequeId))
                {
                    item.AmountPaid = totalAmount;
                }

                if (item.AmountPaid < totalAmount)
                {
                    item.Status = "Partially Paid Transaction";
                }

                unitOfWork.SalesOrderPaymentRepo.SaveChanges();
                unitOfWork.SalesOrderRepo.SaveChanges();
                vm.SalesOrder = item;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return vm.SalesOrder;
        }

        public int Delete(string salesOrderId)
        {
            try
            {
                db.SalesOrders.Remove(db.SalesOrders.FirstOrDefault(x => x.Id == salesOrderId));
                return db.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return 0;
        }

        public SalesOrders Initialize()
        {
            return Insert(new Data.Models.SalesOrders()
            {
                Id = Guid.NewGuid().ToString(),
                OrderNumber = new Random().Next(1, 1000000).ToString("d6"),
                DateCreated = DateTime.Now,
                SalesOrderDetails = new List<SalesOrderDetails>() { },
                Status = "Initial Transaction",
                CreatedBy = httpContextAccessor.HttpContext.User.GetUserId()
            });
        }

        public void TenderTransaction(int? salesOrderId)
        {
            try
            {
                var sales = db.SalesOrders.Find(salesOrderId);
                sales.Status = "Tendered Transaction";
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        public void TenderTransaction(SalesOrders salesOrders)
        {
            try
            {
                var sales = db.SalesOrders.Find(salesOrders.Id);
                sales.CustomerName = salesOrders.CustomerName;
                sales.AmountPaid = salesOrders.AmountPaid;
                sales.Status = "Tendered Transaction";
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        public void CancelTransaction(string salesOrderId)
        {
            try
            {
                db.SalesOrders.Remove(db.SalesOrders.Find(salesOrderId));
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }
    }
}
