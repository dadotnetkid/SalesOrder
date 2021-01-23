using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private IChequeService chequeService;
        private ISalesOrderService salesOrderService;

        public ReportsController(IChequeService chequeService, ISalesOrderService salesOrderService)
        {
            this.chequeService = chequeService;
            this.salesOrderService = salesOrderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("Reports/cheques")]
        public IActionResult Cheques()
        {
            return View();
        }

        public IActionResult ChequesPartial()
        {
            var res = chequeService.Get(x => EF.Functions.DateDiffDay(x.ChequeDate, DateTime.Now) >= 15);
            return PartialView(res);
        }

        public IActionResult UnsettledOrders()
        {
            return View();
        }
        public IActionResult UnsettledOrdersPartial()
        {
            var model = salesOrderService.Get(x => EF.Functions.DateDiffDay(x.DateCreated, DateTime.Now) >= 15 && x.AmountPaid < x.SalesOrderDetails.Sum(m => (m.PurchaseAmount ?? 0)));
            return PartialView(model);
        }
    }
}
