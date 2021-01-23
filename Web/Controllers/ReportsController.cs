using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private IChequeService chequeService;

        public ReportsController(IChequeService chequeService)
        {
            this.chequeService = chequeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("Reports/cheques")]
        public IActionResult Cheques()
        {
            return View();
        }

        public IActionResult ChequesPartial()
        {
            var res = chequeService.Get(x => EF.Functions.DateDiffDay( x.ChequeDate, DateTime.Now) >= 15);
            return PartialView(res);
        }
    }
}
