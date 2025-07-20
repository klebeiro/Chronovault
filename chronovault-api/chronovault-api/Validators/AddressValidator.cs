using chronovault_api.DTOs;
using FluentValidation;

namespace chronovault_api.Validators
{
    public class AddressDTOValidator : AbstractValidator<AddressDTO>
    {
        public AddressDTOValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(100).WithMessage("Street must not exceed 100 characters.");

            RuleFor(x => x.Number)
                .Must(n => int.TryParse(n, out _)).WithMessage("Invalid number, must be an integer.")
                .MaximumLength(20).WithMessage("Number must not exceed 50 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required.")
                .MaximumLength(10).WithMessage("ZipCode must not exceed 10 characters.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(50).WithMessage("State must not exceed 50 characters.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");

            RuleFor(x => x.Complement)
                .MaximumLength(50).WithMessage("Complement must not exceed 50 characters.");
        }
    }
}