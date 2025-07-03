using FluentValidation;
using UserDomain;

namespace UserApplication.Validators;

public class ProfileValidator : AbstractValidator<Profile>
{
    public ProfileValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(id => id != Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is Required")
            .Length(2, 50).WithMessage("First Name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is Required")
            .Length(2, 50).WithMessage("Last Name must be between 2 and 50 characters.");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is Required")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone Number must be valid.");
    }
}