using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(p => p.Description).MinimumLength(2);
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Description).NotNull();
            RuleFor(p => p.DailyPrice).NotEmpty();
            RuleFor(p => p.DailyPrice).GreaterThan(0);
            RuleFor(p => p.ModelYear).LessThan(DateTime.Now.Year);
            RuleFor(p => p.ModelYear).NotEmpty();
            RuleFor(p => p.BrandId).NotEmpty();
            RuleFor(p => p.ColorId).NotEmpty();
        }
    }
}
