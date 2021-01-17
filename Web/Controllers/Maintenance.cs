using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Data.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class Maintenance : Controller
    {
        private ModelDb db;

        public Maintenance(ModelDb db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Users

        public ActionResult Users()
        {
            return View();
        }

        #endregion
    }
}
