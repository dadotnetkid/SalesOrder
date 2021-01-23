using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using Services.VM;

namespace Web.Controllers
{
    public class SalesOrderController : Controller
    {
        #region fields
        private ICustomerService customerService;
        private IProductService productService;
        private ISalesOrderService salesOrderService;
        private ISalesOrderDetailService salesOrderDetailService;
        private IChequeService chequeService;
        #endregion

        #region ctor
        public SalesOrderController(IProductService productService,
            ICustomerService customerService,
            ISalesOrderService salesOrderService,
            ISalesOrderDetailService salesOrderDetailService,
            IChequeService chequeService
        )
        {
            this.productService = productService;
            this.customerService = customerService;
            this.salesOrderService = salesOrderService;
            this.salesOrderDetailService = salesOrderDetailService;
            this.chequeService = chequeService;
        }


        #endregion
        public IActionResult Index()
        {
            return View();
        }
        [Route("sales-order/detail/{salesOrderId?}")]
        public IActionResult SalesOrderDetailGridPartial(string salesOrderId)
        {
            if (salesOrderId == null)
            {
                var res = salesOrderService.Initialize();
                return RedirectToAction("SalesOrderDetailGridPartial", new { salesOrderId = res.Id });
            }
            var model = salesOrderService.Find(x => x.Id == salesOrderId);
            return View(model);
        }

        public IActionResult AddEditSalesOrderDetailModalPartial(SalesOrderDetailVM model)
        {
            model.SalesOrderDetails = salesOrderDetailService.Find(x => x.Id == model?.SalesOrderDetailId);
            return PartialView(model);
        }

        public IActionResult AddSalesOrderDetailPartial(SalesOrderDetailVM model)
        {
            if (!ModelState.IsValid)
                return PartialView("AddEditSalesOrderDetailModalPartial", model);
            if (string.IsNullOrEmpty(model.SalesOrderDetails.Id))
                salesOrderDetailService.Insert(model.SalesOrderDetails);
            else
                salesOrderDetailService.Update(model.SalesOrderDetails);
            model.isSuccess = true;
            return PartialView("AddEditSalesOrderDetailModalPartial", model);
        }
        [HttpPost]
        public IActionResult TenderTransactionModalPartial(string salesOrderId)
        {
            var model = new SalesOrderVM()
            {
                SalesOrder = salesOrderService.Find(x => x.Id == salesOrderId),
                saleOrderId = salesOrderId
            };


            if (string.IsNullOrEmpty(model?.SalesOrder?.PaymentMethod))
            {
                model.SalesOrder.PaymentMethod = "Cash";
            }
            return PartialView(model);
        }

        public IActionResult TenderTransactionPartial(SalesOrderVM model)
        {
            model.Cheques = chequeService.Get();
            if (!ModelState.IsValid)
                return PartialView("TenderTransactionModalPartial", model);
            salesOrderService.Update(model);
            model.isSuccess = true;

            return PartialView("TenderTransactionModalPartial", model);
        }

        public IActionResult DeleteSalesOrderGridPartial(string salesOrderId, string saledOrderDetailId)
        {
            salesOrderDetailService.Delete(x => x.Id == saledOrderDetailId);
            return RedirectToAction("SalesOrderDetailGridPartial", new { salesOrderId = salesOrderId });
        }

        public IActionResult CancelTransaction(int id)
        {
            salesOrderService.CancelTransaction(id);
            return RedirectToAction("Index");
        }
    }
}
