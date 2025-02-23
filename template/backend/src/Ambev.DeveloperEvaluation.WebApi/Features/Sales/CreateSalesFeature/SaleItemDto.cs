namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;
public class SaleItemDto
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
}
