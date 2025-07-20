using chronovault_api.DTOs.Request;
using FluentValidation;

namespace chronovault_api.Validators
{
    public class OrderItemCreateDTOValidator : AbstractValidator<OrderItemCreateDTO>
    {
        public OrderItemCreateDTOValidator()
        {
            RuleFor(oi => oi.Quantity)
                .NotEmpty().WithMessage("Quantity cannot be null.");

            RuleFor(oi => oi.ProductId)
                .NotEmpty().WithMessage("Product Id cannot be null.");
        }
    }
}
