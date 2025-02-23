using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSales
{
    public class DeleteSalesProfile : Profile
    {
        public DeleteSalesProfile()
        {
            CreateMap<string, Application.Sales.DeleteSales.DeleteSalesCommand>()
                .ConstructUsing(id => new Application.Sales.DeleteSales.DeleteSalesCommand(id));
        }
    }
}
