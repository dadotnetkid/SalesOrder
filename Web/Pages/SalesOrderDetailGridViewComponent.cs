using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Web.Pages
{
    public class SalesOrderDetailGridViewComponent : ViewComponent
    {
        private ISalesOrderDetailService salesOrderDetailService;

        public SalesOrderDetailGridViewComponent(ISalesOrderDetailService salesOrderDetailService)
        {
            this.salesOrderDetailService = salesOrderDetailService;
        }
        public async Task<IViewComponentResult> InvokeAsync(SalesOrders salesOrders)
        {
            var model = salesOrderDetailService.SalesOrderDetails(x => x.SalesOrderId == salesOrders?.Id,includeProperties: "SalesOrder");
            ViewData["orderId"] = salesOrders?.Id;
            return await Task.FromResult(View("SalesOrderDetailGrid", model));
        }
    }
}
