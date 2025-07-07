using chronovault_api.DTOs.Request;
using FluentValidation;

namespace chronovault_api.Validators
{
    public class OrderCreateDTOValidator : AbstractValidator<OrderCreateDTO>
    {
        public OrderCreateDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(x => x.OrderItems)
                .NotEmpty().WithMessage("OrderItems is required.")
                .Must(x => x != null && x.Any()).WithMessage("OrderItems must contain at least one item.");

            RuleForEach(x => x.OrderItems)
                .SetValidator(new OrderItemCreateDTOValidator());

            RuleFor(x => x.ShippingCost)
                .GreaterThanOrEqualTo(0).WithMessage("ShippingCost must be greater than or equal to 0.");

            RuleFor(x => x.DiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("DiscountAmount must be greater than or equal to 0.");

            RuleFor(x => x.ShippingAddress)
                .MaximumLength(200).WithMessage("ShippingAddress must not exceed 200 characters.");
        }
    }

    public class OrderUpdateDTOValidator : AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateDTOValidator()
        {
            RuleFor(x => x.ShippingCost)
                .GreaterThanOrEqualTo(0).WithMessage("ShippingCost must be greater than or equal to 0.");

            RuleFor(x => x.DiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("DiscountAmount must be greater than or equal to 0.");

            RuleFor(x => x.ShippingAddress)
                .MaximumLength(200).WithMessage("ShippingAddress must not exceed 200 characters.");
        }
    }
}