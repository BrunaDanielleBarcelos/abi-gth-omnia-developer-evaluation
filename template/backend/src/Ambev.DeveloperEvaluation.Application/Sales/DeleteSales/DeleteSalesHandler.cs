using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;


namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;

public class DeleteSalesHandler : IRequestHandler<DeleteSalesCommand, DeleteSalesResponse>
{
    private readonly ISalesRepository _salesRepository;
    public DeleteSalesHandler(
    ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public async Task<DeleteSalesResponse> Handle(DeleteSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSalesValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _salesRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        return new DeleteSalesResponse { Success = true };
    }

}
