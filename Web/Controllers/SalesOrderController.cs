using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class SalesOrderController : Controller
    {
        private ICustomerService customerService;
        private IProductService productService;
        private ISalesOrderService salesOrderService;
        private ISalesOrderDetailService salesOrderDetailService;

        public SalesOrderController(IProductService productService, ICustomerService customerService, ISalesOrderService salesOrderService,
        ISalesOrderDetailService salesOrderDetailService
        )
        {
            this.productService = productService;
            this.customerService = customerService;
            this.salesOrderService = salesOrderService;
            this.salesOrderDetailService = salesOrderDetailService;

        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Render add edit form for the sales order Id
        /// </summary>
        /// <param name="orderId">order Id from salesordergrid</param>
        /// <param name="methodType">Add/Edit</param>
        /// <returns></returns>
        public IActionResult AddEditSalesOrderPartial(int? orderId, string methodType)
        {
            if (methodType == "Add")
            {
                var salesOrder = salesOrderService.Initialize();
                orderId = salesOrder.Id;
                return RedirectToAction("AddEditSalesOrderPartial", new { orderId = orderId, methodType = "Edit" });
            }


            return View(salesOrderService.Find(x => x.Id == orderId));
        }
        public IActionResult SalesOrderGridPartial()
        {
            return ViewComponent("SalesOrderGrid");
        }

        public IActionResult AddEditAddSalesOrderDetailModalPartial(int? orderId, int? salesOrderDetailId)
        {
            return ViewComponent("AddEditAddSalesOrderDetailModalPartial", new { orderId = orderId, salesOrderDetailId = salesOrderDetailId });
        }

        public IActionResult AddSalesOrderGridPartial(SalesOrderDetails model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ViewComponent("AddEditAddSalesOrderDetailModalPartial", new { salesOrderDetails = model, url = Url.Action("AddSalesOrderGridPartial") });
                salesOrderDetailService.Insert(model);
                ViewData["isSuccess"] = true;
            }
            catch (Exception e)
            {

            }
            return ViewComponent("AddEditAddSalesOrderDetailModalPartial", new { orderId = model.SalesOrderId, saledOrderDetailId = model.Id });
        }

        public IActionResult UpdateSalesOrderGridPartial(SalesOrderDetails model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ViewComponent("AddEditAddSalesOrderDetailModalPartial", new { salesOrderDetails = model, url = Url.Action("UpdateSalesOrderGridPartial") });
                salesOrderDetailService.Update(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return ViewComponent("AddEditAddSalesOrderDetailModalPartial", new { orderId = model.SalesOrderId, saledOrderDetailId = model.Id });
        }

        public IActionResult DeleteSalesOrderGridPartial(int? orderId, int? saledOrderDetailId)
        {
            salesOrderDetailService.Delete(x => x.Id == saledOrderDetailId);
            return RedirectToAction("AddEditSalesOrderPartial",new{ orderId = orderId , methodType ="Edit"});
            // AddEditSalesOrderPartial(int ? orderId, string methodType)
        }
        public IActionResult TenderTransaction(SalesOrders salesOrders)
        {
            if (!ModelState.IsValid)
            {
                return View("AddEditSalesOrderPartial", salesOrders);
            }

            salesOrderService.TenderTransaction(salesOrders);
            return RedirectToAction("Index");
        }
        public IActionResult CancelTransaction(int id)
        {
            salesOrderService.CancelTransaction(id);
            return RedirectToAction("Index");
        }
    }
}
