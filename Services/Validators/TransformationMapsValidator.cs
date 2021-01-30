using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using FluentValidation;
using Services.VM;

namespace Services.Validators
{
    public class TransformationMapsValidator : AbstractValidator<TransformationMapsVM>
    {
        public TransformationMapsValidator()
        {
            RuleFor(x => x.TransformationMap.SourceProductId)
                .NotEmpty()
                .WithMessage("Source Product is required");
            RuleFor(x => x.TransformationMap.DestinationProductId)
                .NotEmpty()
                .WithMessage("Destination Product is required");
            RuleFor(x => x.TransformationMap.Quantity)
                .NotEmpty()
                .WithMessage("Quantity is required");
            RuleFor(x => x.TransformationMap.ToleranceAmount)
                .NotEmpty()
                .WithMessage("Tolerance Amount is required");
        }
    }
}
