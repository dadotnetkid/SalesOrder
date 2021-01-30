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
    [Authorize]
    public class SalesOrderPaymentsController : Controller
    {
        private ISalesOrderPaymentService salesOrderPaymentService;


        public SalesOrderPaymentsController(ISalesOrderPaymentService salesOrderPaymentService)
        {
            this.salesOrderPaymentService = salesOrderPaymentService;

        }

        #region Unsettled Payments
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaymentSalesOrderGridPartial()
        {
            return PartialView(salesOrderPaymentService.GetUnsettledPayments());
        }
        [HttpPost]
        public IActionResult BulkSettlePaymentPartial(SalesOrderPaymentsVM model)
        {
            model.Customers = salesOrderPaymentService.GetCustomers();
            model.Cheques = salesOrderPaymentService.GetCheques();

            if (!ModelState.IsValid)
                return PartialView("PaymentSalesOrderDetail", model);

            //Getting result of service
            var result = salesOrderPaymentService.Insert(model);

            //return partial view with error if the service fail in trycatch
            if (!result.Succeeded)
            {
                ViewData["Errors"] = result.Errors;
                return PartialView("PaymentSalesOrderDetail", model);
            }


            return Json(new { isSuccess = true });
        }

        public IActionResult PaymentSalesOrderDetail(SalesOrderPaymentsVM model)
        {

            model.Customers = salesOrderPaymentService.GetCustomers();
            model.Cheques = salesOrderPaymentService.GetCheques();
            return PartialView(model);
        }


        #endregion

        #region Payment History
        public IActionResult PaymentHistory()
        {
            return View();
        }

        public IActionResult PaymentHistoryPartial()
        {
            return PartialView(salesOrderPaymentService.GetPaymentHistory());
        }


        #endregion

    }
}
