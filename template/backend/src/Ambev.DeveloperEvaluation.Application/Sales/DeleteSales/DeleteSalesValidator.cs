using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;

public class DeleteSalesValidator : AbstractValidator<DeleteSalesCommand>
{
    public DeleteSalesValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID is required");
    }
}

