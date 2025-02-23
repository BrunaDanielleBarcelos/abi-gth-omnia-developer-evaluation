using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Profile for mapping GetSales feature requests to commands
/// </summary>
public class GetSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSales feature
    /// </summary>
    public GetSalesProfile()
    {
        CreateMap<string, Application.Sales.GetSales.GetSalesCommand>()
            .ConstructUsing(id => new Application.Sales.GetSales.GetSalesCommand(id));
    }
}
