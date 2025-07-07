using chronovault_api.DTOs.Request;
using FluentValidation;

namespace chronovault_api.Validators
{
    public class OrderItemCreateDTOValidator : AbstractValidator<OrderItemCreateDTO>
    {
        public OrderItemCreateDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .GreaterThan(0).WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }

    public class OrderItemUpdateDTOValidator : AbstractValidator<OrderItemUpdateDTO>
    {
        public OrderItemUpdateDTOValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}