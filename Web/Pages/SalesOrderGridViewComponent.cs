using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

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

            return await Task.FromResult(View("SalesOrderGrid", salesOrderService.Orders()));
        }
    }
}
