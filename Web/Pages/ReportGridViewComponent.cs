using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Web.Pages
{
    public class ReportGridViewComponent : ViewComponent
    {
        private ISalesOrderService salesOrderService;

        public ReportGridViewComponent(ISalesOrderService salesOrderService)
        {
            this.salesOrderService = salesOrderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View("ReportGrid",
                salesOrderService.Orders(x =>
                    x.Status == "Initial Transaction" && EF.Functions.DateDiffDay(x.DateCreated, DateTime.Now) >= 15)));
        }
    }
}
