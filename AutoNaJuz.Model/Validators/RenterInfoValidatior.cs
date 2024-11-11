using FluentValidation;

namespace AutoNaJuz.Model.Validators;

public class RenterInfoValidatior : AbstractValidator<RenterInfo>
{
    public RenterInfoValidatior()
    {
        RuleFor(x => x.DriversLicenseIdent)
            .NotEmpty()
            .WithMessage("Drivers license ID is required.");

        RuleFor(x => x.Pesel)
            .NotEmpty()
            .WithMessage("PESEL is required.")
            .Must((info, _) => info.IsPeselValid())
            .WithMessage("PESEL is invalid.");
    }
}