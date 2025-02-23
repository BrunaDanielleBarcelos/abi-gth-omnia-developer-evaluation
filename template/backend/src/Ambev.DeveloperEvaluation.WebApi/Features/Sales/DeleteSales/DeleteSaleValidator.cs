using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSales;

public class DeleteSaleValidator : AbstractValidator<DeleteSaleRequest>
{
    public DeleteSaleValidator()
    {
        RuleFor(x => x.CodigoVenda)
            .NotEmpty()
            .WithMessage("Codigo Venda is required");
    }

}
