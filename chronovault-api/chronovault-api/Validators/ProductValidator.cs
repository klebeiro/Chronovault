using chronovault_api.DTOs.Request;
using chronovault_api.DTOs;
using FluentValidation;

namespace chronovault_api.Validators
{
    public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateDTOValidator()
        {
            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(100).WithMessage("Model must not exceed 100 characters.");

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Brand is required.")
                .MaximumLength(50).WithMessage("Brand must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.StockQuantity)
                .NotEmpty().WithMessage("StockQuantity is required.")
                .GreaterThanOrEqualTo(0).WithMessage("StockQuantity must be greater than or equal to 0.");

            RuleFor(x => x.CaseMaterial)
                .MaximumLength(50).WithMessage("CaseMaterial must not exceed 50 characters.");

            RuleFor(x => x.StrapMaterial)
                .MaximumLength(50).WithMessage("StrapMaterial must not exceed 50 characters.");

            RuleFor(x => x.WaterResistance)
                .MaximumLength(50).WithMessage("WaterResistance must not exceed 50 characters.");

            RuleFor(x => x.Warranty)
                .MaximumLength(100).WithMessage("Warranty must not exceed 100 characters.");
        }
    }

    public class ProductUpdateDTOValidator : AbstractValidator<ProductUpdateDTO>
    {
        public ProductUpdateDTOValidator()
        {
            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(100).WithMessage("Model must not exceed 100 characters.");

            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Brand is required.")
                .MaximumLength(50).WithMessage("Brand must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.StockQuantity)
                .NotEmpty().WithMessage("StockQuantity is required.")
                .GreaterThanOrEqualTo(0).WithMessage("StockQuantity must be greater than or equal to 0.");

            RuleFor(x => x.CaseMaterial)
                .MaximumLength(50).WithMessage("CaseMaterial must not exceed 50 characters.");

            RuleFor(x => x.StrapMaterial)
                .MaximumLength(50).WithMessage("StrapMaterial must not exceed 50 characters.");

            RuleFor(x => x.WaterResistance)
                .MaximumLength(50).WithMessage("WaterResistance must not exceed 50 characters.");

            RuleFor(x => x.Warranty)
                .MaximumLength(100).WithMessage("Warranty must not exceed 100 characters.");
        }
    }
}