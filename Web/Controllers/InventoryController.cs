using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Interfaces;

namespace Web.Controllers
{
    public class InventoryController : Controller
    {
        private IInventoryService inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InventoryPartial()
        {
            return PartialView(inventoryService.GetInventories());
        }
    }
}
