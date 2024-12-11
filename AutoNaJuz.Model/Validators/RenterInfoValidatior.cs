using FluentValidation;

namespace AutoNaJuz.Model.Validators;

public class RenterInfoValidatior : AbstractValidator<RenterInfo.RenterInfo>
{
    public RenterInfoValidatior()
    {
        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Email);
        
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(100);
    }
}