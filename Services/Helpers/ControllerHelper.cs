using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    }
}
