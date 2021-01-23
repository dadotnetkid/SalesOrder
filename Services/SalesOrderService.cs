using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using Services.Interfaces;
using Services.VM;

namespace Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private ModelDb db;
        private IHttpContextAccessor httpContextAccessor;

        public SalesOrderService(ModelDb db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
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
                var item = db.SalesOrders.FirstOrDefault(x => x.Id == vm.saleOrderId);
                /*vm.SalesOrder = item;*/
                item.CustomerName = vm.SalesOrder.CustomerName;
                item.AmountPaid = vm.SalesOrder.AmountPaid;
                if (vm.ChequeId != null)
                    item.ChequeInSalesOrder.Add(db.ChequeInSalesOrder.Find(vm.ChequeId));
                item.Status = "Tendered Transaction";
                db.SaveChanges();
                vm.SalesOrder = item;
            }
            catch (Exception e)
            {

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

        public void CancelTransaction(int? salesOrderId)
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
