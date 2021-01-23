using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;

namespace Services.Helpers
{
    public static class ControllerHelper
    {

        public static string IsMenuActive(this ViewContext viewContext, string action, string controller)
        {
            string currentController = viewContext.RouteData.Values["Controller"].ToString()?.ToLower();
            string currentAction = viewContext.RouteData.Values["Action"].ToString()?.ToLower();
            if (action.ToLower() == currentAction && currentController == controller.ToLower())
            {
                return "active";
            }

            return "";
        }

        public static string IsTreeColapse(this ViewContext context, params string[] controller)
        {
            string currentController = context.RouteData.Values["Controller"].ToString()?.ToLower();
            return controller.Any(x => x.ToLower() == currentController) ? "show" : "";
        }

    }
}
