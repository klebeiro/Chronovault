using chronovault_api.DTOs;
using FluentValidation;

namespace chronovault_api.Validators
{
    public class CreditCardValidator : AbstractValidator<CreditCardDTO>
    {
        public CreditCardValidator()
        {
            RuleFor(c => c.CardholderName)
                .NotEmpty().WithMessage("Cardholder name is required.")
                .MaximumLength(100).WithMessage("Cardholder name cannot exceed 100 characters.")
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Cardholder name can only contain letters and spaces.");

            RuleFor(c => c.CardNumber)
                .NotEmpty().WithMessage("Card number is required.")
                .Matches(@"^\d{13,19}$").WithMessage("Card number must contain 13 to 19 digits.");

            RuleFor(c => c.CVV)
                .NotNull().WithMessage("CVV is required.")
                .InclusiveBetween(100, 9999).WithMessage("CVV must be 3 or 4 digits.");

            RuleFor(c => c.ExpiryMonth)
                .NotNull().WithMessage("Expiry month is required.")
                .InclusiveBetween(1, 12).WithMessage("Expiry month must be between 1 and 12.");

            RuleFor(c => c.ExpiryYear)
                .NotNull().WithMessage("Expiry year is required.")
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("Expiry year cannot be in the past.")
                .LessThanOrEqualTo(DateTime.Now.Year + 20).WithMessage("Expiry year is too far in the future.");

            RuleFor(c => c.InstallmentCount)
                .GreaterThan(0).WithMessage("Installment count must be greater than zero.")
                .LessThanOrEqualTo(24).WithMessage("Installment count cannot exceed 24.")
                .When(c => c.InstallmentCount.HasValue);

            RuleFor(c => c)
                .Must(BeValidExpiryDate)
                .WithMessage("Credit card is expired.")
                .WithName("ExpiryDate")
                .When(c => c.ExpiryMonth.HasValue && c.ExpiryYear.HasValue);
        }

        private bool BeValidExpiryDate(CreditCardDTO creditCard)
        {
            if (!creditCard.ExpiryMonth.HasValue || !creditCard.ExpiryYear.HasValue)
                return true;

            var currentDate = DateTime.Now;
            var expiryDate = new DateTime(creditCard.ExpiryYear.Value, creditCard.ExpiryMonth.Value, 1)
                .AddMonths(1).AddDays(-1);

            return expiryDate >= currentDate;
        }
    }
}
