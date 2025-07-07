using chronovault_api.DTOs.Request;
using FluentValidation;

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

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Phone must not exceed 20 characters.");

            RuleFor(x => x.CPF)
                .MaximumLength(14).WithMessage("CPF must not exceed 14 characters.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("BirthDate is required.")
                .LessThan(DateTime.Now).WithMessage("BirthDate must be in the past.");

            RuleFor(x => x.PreferredBrand)
                .MaximumLength(50).WithMessage("PreferredBrand must not exceed 50 characters.");

            RuleFor(x => x.MinimumPriceRange)
                .GreaterThanOrEqualTo(0).WithMessage("MinimumPriceRange must be greater than or equal to 0.");

            RuleFor(x => x.MaximumPriceRange)
                .GreaterThanOrEqualTo(0).WithMessage("MaximumPriceRange must be greater than or equal to 0.")
                .GreaterThanOrEqualTo(x => x.MinimumPriceRange).WithMessage("MaximumPriceRange must be greater than or equal to MinimumPriceRange.");
        }
    }

    public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Phone must not exceed 20 characters.");

            RuleFor(x => x.PreferredBrand)
                .MaximumLength(50).WithMessage("PreferredBrand must not exceed 50 characters.");

            RuleFor(x => x.MinimumPriceRange)
                .GreaterThanOrEqualTo(0).WithMessage("MinimumPriceRange must be greater than or equal to 0.");

            RuleFor(x => x.MaximumPriceRange)
                .GreaterThanOrEqualTo(0).WithMessage("MaximumPriceRange must be greater than or equal to 0.")
                .GreaterThanOrEqualTo(x => x.MinimumPriceRange).WithMessage("MaximumPriceRange must be greater than or equal to MinimumPriceRange.");
        }
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