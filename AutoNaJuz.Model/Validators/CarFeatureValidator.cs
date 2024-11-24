using AutoNaJuz.Model.Car;
using FluentValidation;

namespace AutoNaJuz.Model.Validators;

public class CarFeatureValidator : AbstractValidator<CarFeature>
{
    public CarFeatureValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(256);
    }
}