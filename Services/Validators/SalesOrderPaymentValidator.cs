using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Services.VM;

namespace Services.Validators
{
    public class SalesOrderPaymentValidator : AbstractValidator<SalesOrderPaymentsVM>
    {
        public SalesOrderPaymentValidator()
        {
            RuleFor(x => x.ChequeId)
                .NotEmpty()
                .WithMessage("Cheque is required");
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Customer is required");
        }
    }
}
