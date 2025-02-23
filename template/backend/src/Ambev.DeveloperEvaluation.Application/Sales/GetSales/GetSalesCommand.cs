using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Command for retrieving a user by their ID
/// </summary>
public record GetSalesCommand : IRequest<GetSalesResult>
{
    /// <summary>
    /// The unique identifier of the user to retrieve
    /// </summary>
    public string CodigoVenda { get; }

    /// <summary>
    /// Initializes a new instance of GetUserCommand
    /// </summary>
    /// <param name="id">The ID of the user to retrieve</param>
    public GetSalesCommand(string codigoVenda)
    {
        CodigoVenda = codigoVenda;
    }
}
