using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.VM;

namespace Web.Pages
{
    public class SalesOrderGridViewComponent: ViewComponent
    {
        private ISalesOrderService salesOrderService;

        public SalesOrderGridViewComponent(ISalesOrderService salesOrderService)
        {
            this.salesOrderService = salesOrderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           var salesOrder= salesOrderService.Get();
            return await Task.FromResult(View("~/views/SalesOrder/SalesOrderGrid.cshtml", salesOrder));
        }
    }
}
