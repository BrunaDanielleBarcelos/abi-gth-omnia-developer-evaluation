using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        {
            // Mapeamento de CreateSaleCommand para SaleItem
            CreateMap<CreateSaleCommand, SaleItem>()
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()); // Ignora TotalPrice durante o mapeamento de CreateSaleCommand para SaleItem

            // Mapeamento de SalesEntity para SaleItem
            CreateMap<SalesEntity, SaleItem>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice * (1 - src.Discount / 100m))); // Cálculo do TotalPrice

            // Mapeamento de SaleItem para CreateSaleResult
            CreateMap<SaleItem, CreateSaleResult>();
     
        }
    }
}
