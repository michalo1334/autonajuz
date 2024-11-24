using FluentValidation;

namespace AutoNaJuz.Model.Validators;

public class CarValidator : AbstractValidator<Car.Car>
{
    public CarValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.ProductionYear)
            .NotEmpty()
            .LessThan(DateTime.Now);

        RuleFor(x => x.SeatCount)
            .GreaterThan(0);

        RuleFor(x => x.DoorCount)
            .GreaterThan(0);
        
        RuleFor(x => x.RentCostPerDay)
            .GreaterThanOrEqualTo(0m);
    }
}