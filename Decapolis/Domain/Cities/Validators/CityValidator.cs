using System;
using Domain.Cities.Entities;
using FluentValidation;

namespace Domain.Cities.Validators
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
            RuleFor(e => e.Name)
                .NotEmpty();
        }
    }
}