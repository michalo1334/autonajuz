using FluentValidation;

namespace AutoNaJuz.Model.Validators
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            // RuleFor(car => car.Make).NotEmpty().WithMessage("Make is required.");
            // RuleFor(car => car.Model).NotEmpty().WithMessage("Model is required.");
            // RuleFor(car => car.Year).InclusiveBetween(1886, 9999).WithMessage("Year must be between 1886 and 9999.");
            // RuleFor(car => car.VIN).Length(17).WithMessage("VIN must be 17 characters long.");
        }
    }
}