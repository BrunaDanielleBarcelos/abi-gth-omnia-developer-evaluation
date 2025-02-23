using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Represents a request to create a new sale in the system.
/// </summary>
public class CreateSalesRequest 
{
    public List<SaleItemDto> Items { get; set; }
}
