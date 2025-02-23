using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {

        CreateMap<SaleItemDto, SaleItem>();

        CreateMap<CreateSalesRequest, CreateSaleCommand>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        
        CreateMap<CreateSaleResult, CreateSaleResponse>();
    }
}
