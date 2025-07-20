using chronovault_api.DTOs.Request;
using chronovault_api.Models.Enums;
using FluentValidation;

namespace chronovault_api.Validators
{
    public class OrderCreateDTOValidator : AbstractValidator<OrderCreateDTO>
    {
        public OrderCreateDTOValidator()
        {
            RuleFor(o => o.OrderItems)
                .NotEmpty().WithMessage("Order must have at least one item.")
                .Must(items => items != null && items.Count > 0)
                .WithMessage("Order items cannot be null or empty.");

            RuleForEach(o => o.OrderItems)
                .SetValidator(new OrderItemCreateDTOValidator());

            RuleFor(o => o.PaymentMethod)
                .IsInEnum().WithMessage("Invalid payment method.");

            When(o => o.PaymentMethod == PaymentMethod.CreditCard, () =>
            {
                RuleFor(o => o.CreditCardInformation)
                    .NotNull().WithMessage("Credit card information is required for credit card payments.")
                    .SetValidator(new CreditCardValidator()!);
            });

            When(o => o.PaymentMethod != PaymentMethod.CreditCard, () =>
            {
                RuleFor(o => o.CreditCardInformation)
                    .Null().WithMessage("Credit card information should not be provided for non-credit card payments.");
            });
        }
    }
}