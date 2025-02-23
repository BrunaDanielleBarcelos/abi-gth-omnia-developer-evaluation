using Ambev.DeveloperEvaluation.Application.Sales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;

/// <summary>
/// Validator for GetUserRequest
/// </summary>
public class GetSaleRequestValidator : AbstractValidator<GetSaleByIdRequest>
{
    /// <summary>
    /// Initializes validation rules for GetUserRequest
    /// </summary>
    public GetSaleRequestValidator()
    {
        RuleFor(x => x.CodigoVenda)
            .NotEmpty()
            .WithMessage("User CodigoVenda is required");
    }
}
