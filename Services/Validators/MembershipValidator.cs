﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using Services.VM;

namespace Services.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        private ModelDb db;


        public RegisterViewModelValidator(ModelDb db)
        {
            this.db = db;
            RuleFor(x => x.UserName)
                .NotNull()
                .WithMessage("Username is required")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Invalid email address")
                .Must(EmailIsExist)
                .WithMessage("Email address is exist");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Password is required")
                .Length(6)
                .WithMessage("Password is to small");
        }

        bool EmailIsExist(string name)
        {
            return !db.AspNetUsers.Any(x => x.Email == name);
        }
    }
}
