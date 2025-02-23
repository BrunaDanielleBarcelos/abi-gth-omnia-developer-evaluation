using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Handler for processing GetUserCommand requests
/// </summary>
public class GetSalesHandler : IRequestHandler<GetSalesCommand, GetSalesResult>
{
    private readonly ISalesRepository _salesRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetSalesHandler
    /// </summary>
    /// <param name="salesRepository">The sales repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetSalesCommand</param>
    public GetSalesHandler(
        ISalesRepository salesRepository,
        IMapper mapper)
    {
        _salesRepository = salesRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSalesCommand request
    /// </summary>
    /// <param name="request">The GetSales command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sales details if found</returns>
    public async Task<GetSalesResult> Handle(GetSalesCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSalesCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sales = await _salesRepository.GetByIdAsync(request.CodigoVenda, cancellationToken);
        if (sales == null)
            throw new KeyNotFoundException($"Sale with ID {request.CodigoVenda} not found");

        return _mapper.Map<GetSalesResult>(sales);
    }
}
