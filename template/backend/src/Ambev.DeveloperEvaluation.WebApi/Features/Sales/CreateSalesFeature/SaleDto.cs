namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

public class SaleDto
{
    public Guid Id { get; set; }
    public List<SaleItemDto> Items { get; set; }
    public decimal TotalAmount { get; set; }
}
