using chronovault_api.DTOs.Request;
using FluentValidation;

namespace chronovault_api.Validators
{
    public class ImageCreateDTOValidator : AbstractValidator<ImageCreateDTO>
    {
        public ImageCreateDTOValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .MaximumLength(200).WithMessage("Url must not exceed 200 characters.");

            RuleFor(x => x.Order)
                .NotEmpty().WithMessage("Order is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Order must be greater than or equal to 0.");
        }
    }

    public class ImageUpdateDTOValidator : AbstractValidator<ImageUpdateDTO>
    {
        public ImageUpdateDTOValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .MaximumLength(200).WithMessage("Url must not exceed 200 characters.");

            RuleFor(x => x.Order)
                .NotEmpty().WithMessage("Order is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Order must be greater than or equal to 0.");
        }
    }
}