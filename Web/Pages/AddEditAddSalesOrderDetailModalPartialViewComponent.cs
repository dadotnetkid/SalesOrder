using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Web.Pages
{
    public class AddEditAddSalesOrderDetailModalPartialViewComponent : ViewComponent
    {
        private ISalesOrderDetailService salesOrderDetailService;

        public AddEditAddSalesOrderDetailModalPartialViewComponent(ISalesOrderDetailService salesOrderDetailService)
        {
            this.salesOrderDetailService = salesOrderDetailService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? orderId, int? salesOrderDetailId, string url, SalesOrderDetails salesOrderDetails)
        {
            if (!string.IsNullOrEmpty(url))
            {
                ViewData["url"] = url;
                return await Task.FromResult(View("AddEditAddSalesOrderDetailModalPartial", salesOrderDetails));
            }



            ViewData["orderId"] = orderId;
            var model = salesOrderDetailService.Find(x => x.Id == salesOrderDetailId);
            ViewData["url"] = Url.Action("AddSalesOrderGridPartial", "SalesOrder", new { salesOrderId = orderId });
            if (model != null)
                ViewData["url"] = Url.Action("UpdateSalesOrderGridPartial", "SalesOrder", new { salesOrderId = orderId });
            return await Task.FromResult(View("AddEditAddSalesOrderDetailModalPartial", model??new  SalesOrderDetails() ));
        }

    }
}
