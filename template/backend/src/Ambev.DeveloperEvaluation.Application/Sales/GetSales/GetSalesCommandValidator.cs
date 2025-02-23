using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Validator for GetUserCommand
/// </summary>
public class GetSalesCommandValidator : AbstractValidator<GetSalesCommand>
{
    /// <summary>
    /// Initializes validation rules for GetUserCommand
    /// </summary>
    public GetSalesCommandValidator()
    {
        RuleFor(x => x.CodigoVenda)
            .NotEmpty()
            .WithMessage("User CodigoVenda is required");
    }
}
