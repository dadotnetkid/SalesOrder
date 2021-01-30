using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using FluentValidation;
using FluentValidation.Results;
using Services.Interfaces;
using Services.VM;

namespace Services.Validators
{
    public class SalesOrderValidator : AbstractValidator<SalesOrderVM>
    {
        private IChequeService chequeService;

        public SalesOrderValidator(IChequeService chequeService)
        {
            this.chequeService = chequeService;
            RuleFor(x => x.SalesOrder.CustomerId)
          .NotNull()
             .WithMessage("Customer Name is required");
            RuleFor(x => x.SalesOrder.AmountPaid)
                .Must(AmountPaid)
                .WithMessage("Check amount paid");
            RuleFor(x => x.ChequeId)
                .Must(cheque)
                .WithMessage("Invalid Cheque");
        }

        private bool cheque(SalesOrderVM vm, string chequeId)
        {
            if (vm.SalesOrder.PaymentMethod == "Cash")
                return true;
            return !string.IsNullOrEmpty(chequeId);

        }
        /*  private bool cheque(SalesOrderVM vm, ICollection<ChequeInSalesOrder> arg2)
          {
              if (vm.SalesOrder.PaymentMethod == "Cash")
                  return true;
              if (!chequeService.Get(x => x.ChequeInSalesOrder.Any(m => m.SalesOrderId == vm.saleOrderId)).Any())
                  return false;
              return false;
          }*/

        private bool AmountPaid(SalesOrderVM vm, decimal? amount)
        {
            if (vm.SalesOrder.PaymentMethod == "Cheque")
                return true;
            if (amount <= 0)
                return false;
            if (amount == null)
                return false;
            return true;
        }
    }

    public class SalesOrderDetailsValidator : AbstractValidator<SalesOrderDetailVM>
    {
        public SalesOrderDetailsValidator()
        {
            RuleFor(x => x.SalesOrderDetails.ProductName)
                .NotNull()
                .WithMessage("Product is required");

            RuleFor(x => x.SalesOrderDetails.Qty)
                .NotNull()
                .WithMessage("QTY is required")
                .GreaterThan(0)
                .WithMessage("Quantity must not less than 1");




        }
    }
}
