using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Services.Interfaces;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerService customerService;
        private IProductService productService;


        public HomeController(ICustomerService customerService,IProductService productService)
        {
            this.customerService = customerService;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            var res = this.productService.Products();
            return View();
        }
        [HttpPost]
        public IActionResult Index(SalesOrderDetails item)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return View(item);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
