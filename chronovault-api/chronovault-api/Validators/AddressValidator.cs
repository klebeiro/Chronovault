using chronovault_api.DTOs;
using FluentValidation;

namespace chronovault_api.Validators
{
    public class AddressDTOValidator : AbstractValidator<AddressDTO>
    {
        public AddressDTOValidator()
        {
            RuleFor(x => x.Street)
                .MaximumLength(100).WithMessage("Street must not exceed 100 characters.");

            RuleFor(x => x.City)
                .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

            RuleFor(x => x.ZipCode)
                .MaximumLength(10).WithMessage("ZipCode must not exceed 10 characters.");

            RuleFor(x => x.State)
                .MaximumLength(50).WithMessage("State must not exceed 50 characters.");

            RuleFor(x => x.Country)
                .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");
        }
    }
}