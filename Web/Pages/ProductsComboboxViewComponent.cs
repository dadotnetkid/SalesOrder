using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Web.Pages
{
    public class ProductsComboboxViewComponent : ViewComponent
    {
        private IProductService productService;

        public ProductsComboboxViewComponent(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? productId, SalesOrderDetails salesOrderDetails)
        {
            ViewData["productId"] = productId;
            ViewData["products"] = this.productService.Products();
            return await Task.FromResult(View("ProductsCombobox", salesOrderDetails));
        }
    }
}
