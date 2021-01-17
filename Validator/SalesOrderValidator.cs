using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Validator
{
    public class SalesOrderValidator : AbstractValidator<SalesOrders>
    {
        
        public SalesOrderValidator(ModelDb db)
        {
            RuleFor(x => x.CustomerName)
                .NotNull()
                .WithMessage("Customer Name is required");
            RuleFor(x => x.AmountPaid)
                .NotNull()
                .WithMessage("Amount Paid is required")
          
                .GreaterThanOrEqualTo(x => db.SalesOrderDetails.Where(m=>m.SalesOrderId==x.Id).Sum(m=>m.SubTotal))
                .WithMessage("Amount paid must greater than the total amount");
            RuleFor(x => x.SalesOrderDetails)
                .Must(x => x.Any())
                .WithMessage("No entered Item");

        }
        public override ValidationResult Validate(ValidationContext<SalesOrders> context)
        {
            return base.Validate(context);
        }
    }

    public class SalesOrderDetailsValidator : AbstractValidator<SalesOrderDetails>
    {
        public SalesOrderDetailsValidator()
        {

            RuleFor(x => x.ProductName)
                .NotNull()
                .WithMessage("Product is required");
            RuleFor(x => x.Qty)

                .NotNull()
                .WithMessage("Quantity is Required")
                .GreaterThan(0)
                .WithMessage("Quantity must not less than 1")
                ;

        }
    }
}
