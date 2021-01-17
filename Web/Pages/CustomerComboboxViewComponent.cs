using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Web.Pages
{
 
    public class CustomerComboboxViewComponent : ViewComponent
    {
        private ICustomerService customerService;

        public CustomerComboboxViewComponent(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string componentName,string customerName,SalesOrders salesOrders)
        {
            ViewData["componentName"] = componentName;
            ViewData["customerName"] = customerName;
            ViewData["customers"] = customerService.Customers();
            return await Task.FromResult(View("CustomerCombobox", salesOrders));
        }
    }
}
