using FluentValidation;

namespace AutoNaJuz.Model.Validators;

public class CarRentalValidator : AbstractValidator<CarRental.CarRental>
{
    public CarRentalValidator()
    {
        RuleFor(x => x.From)
            .NotEmpty()
            .LessThan(x => x.To)
            .WithErrorCode("InvalidDateRange");

        RuleFor(x => x.Notes)
            .MaximumLength(3000);
    }
}