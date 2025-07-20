using System.Text.RegularExpressions;
using chronovault_api.DTOs.Request;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace chronovault_api.Validators
{
    public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
    {
        public UserCreateDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Equal(x => x.PasswordConfirmation).WithMessage("Password and Password Confirmation must match.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
                .MaximumLength(20).WithMessage("Phone must not exceed 20 characters.");

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("CPF is required.")
                .MaximumLength(14).WithMessage("CPF must not exceed 14 characters.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("BirthDate is required.");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("Address is required.");
        }

        public class UserCredentialDTOValidator : AbstractValidator<UserCredentialDTO>
        {
            public UserCredentialDTOValidator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Email is not valid.");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
            }
        }
    }
}