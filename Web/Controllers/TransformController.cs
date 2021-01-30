using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;
using Services.VM;

namespace Web.Controllers
{
    [Authorize]
    public class TransformController : Controller
    {
        private ITransformMapService transformMapService;

        public TransformController(ITransformMapService transformMapService)
        {
            this.transformMapService = transformMapService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TransformMapGridPartial()
        {

            return PartialView(transformMapService.Get());
        }

        public IActionResult TransformMapDetailPartial(TransformationMapsVM model)
        {
            model.Products = transformMapService.GetProducts();
            model.TransformationMap = transformMapService.Find(model.TransformationMapId);
            return PartialView(model);
        }

        public IActionResult AddEditTransformMapDetail(TransformationMapsVM model)
        {
            model.Products = transformMapService.GetProducts();

            if (!ModelState.IsValid)
                return PartialView("TransformMapDetailPartial", model);

            var results = transformMapService.AddUpdate(model);

            if (!results.Succeeded)
            {
                ViewData["Errors"] = results.Errors;
                return PartialView("TransformMapDetailPartial", model);
            }

            return Json(new { isSuccess = true });
        }

        public IActionResult DeleteTransformMapPartial(int transformationMapId)
        {
            var result = transformMapService.DeleteTransformMap(transformationMapId);
            if (!result.Succeeded)
                ViewData["Errors"] = result.Errors;
            return PartialView("TransformMapGridPartial", transformMapService.Get());
        }
    }
}
