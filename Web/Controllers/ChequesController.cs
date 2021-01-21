using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Services.Interfaces;
using Web.Pages;

namespace Web.Controllers
{
    public class ChequesController : Controller
    {
        private IChequeService chequeService;

        public ChequesController(IChequeService chequeService)
        {
            this.chequeService = chequeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChequeGridViewPartial()
        {
            return ViewComponent(typeof(ChequeGridViewComponent));
        }

        public IActionResult AddEditChequePartial(string chequeId)
        {
            var model = chequeService.Find(x => x.Id == chequeId);
            return PartialView("AddEditChequePartial", model);
        }
        [HttpPost]
        public IActionResult SaveChequePartial(Cheques model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView("AddEditChequePartial", model);
                if (model?.Id == null)
                {
                    chequeService.Insert(model);
                }
                else
                {
                    chequeService.Update(model);
                }
            }
            catch (Exception e)
            {

            }
            ViewData["isSuccess"] = true;
            return PartialView("AddEditChequePartial", model);
        }


    }
}
