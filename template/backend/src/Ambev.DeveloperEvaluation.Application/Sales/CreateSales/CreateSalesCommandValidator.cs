using Ambev.DeveloperEvaluation.Application.Sales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    /// <summary>
    /// Validator for CreateSaleCommand that defines validation rules for sale creation command.
    /// </summary>
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Items: Must contain at least one item
        /// - Item Quantity: Must be greater than 0
        /// - Item ProductName: Required, must be between 3 and 50 characters
        /// - Item UnitPrice: Must be a positive number
        /// - Item Discount: Must be between 0 and 100 (inclusive)
        /// - Item TotalPrice: Should be positive and calculated based on Quantity and UnitPrice
        /// </remarks>
        public CreateSaleCommandValidator()
        {
            RuleFor(command => command.Items)
                .NotEmpty().WithMessage("At least one item is required.");

            RuleForEach(command => command.Items)
               .ChildRules(item =>
               {
                   item.RuleFor(i => i.ProductName)
                   .NotEmpty().Length(3, 50).WithMessage(" Required, must be between 3 and 50 characters");

                   item.RuleFor(i => i.Quantity)
                       .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

                   item.RuleFor(i => i.UnitPrice)
                       .GreaterThan(0).WithMessage("Unit price must be a positive number.");

                   item.RuleFor(i => i.Discount)
                       .InclusiveBetween(0, 100).WithMessage("Discount must be between 0 and 100.");
               });
        }
    }
}
