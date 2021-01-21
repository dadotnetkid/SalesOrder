using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Web.Pages
{
    public class ChequeGridViewComponent : ViewComponent
    {
        private IChequeService chequeService;

        public ChequeGridViewComponent(IChequeService chequeService)
        {
            this.chequeService = chequeService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Func<Data.Models.Cheques, bool> filter = null)
        {
            return await Task.FromResult(View("~/Views/Cheques/ChequeGridView.cshtml", chequeService.Get(filter)));
        }
    }
}
