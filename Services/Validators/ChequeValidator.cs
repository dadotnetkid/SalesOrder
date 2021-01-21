using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using FluentValidation;

namespace Services.Validators
{
    public class ChequeValidator : AbstractValidator<Cheques>
    {
        public ChequeValidator()
        {
            RuleFor(x => x.AccountName)
                .NotNull()
                .WithMessage("Account name is required");
            RuleFor(x => x.AccountNumber)
                .NotNull()
                .WithMessage("Account no. is required");
            RuleFor(x => x.Amount)
                .NotNull()
                .WithMessage("Amount is required")
                .GreaterThan(0)
                .WithMessage("Amount should above 0");
            RuleFor(x => x.BankAddress)
                .NotNull()
                .WithMessage("Bank address is required");

        }
    }
}
