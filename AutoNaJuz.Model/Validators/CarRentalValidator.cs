using FluentValidation;

namespace AutoNaJuz.Model.Validators
{
    public class CarRentalValidator : AbstractValidator<CarRental.CarRental>
    {
        public CarRentalValidator()
        {
            RuleFor(x => x)
                .Must(x => x.PerHourCost is not null || x.PerDayCost is not null)
                .WithErrorCode("AtLeastOneCostTypeRequired");

            RuleFor(x => x.From)
                .NotEmpty()
                .LessThan(x => x.To)
                .WithErrorCode("InvalidDateRange");

            RuleFor(x => x.Notes)
                .MaximumLength(3000);
        }
    }
}