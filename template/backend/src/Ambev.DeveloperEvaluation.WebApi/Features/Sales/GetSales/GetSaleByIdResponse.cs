namespace Ambev.DeveloperEvaluation.Application.Sales;

public class GetSaleByIdResponse
{
    public Guid Id { get; set; }
    public string Productname { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleItem> Items { get; set; }
    public DateTime CreatedAt { get; set; }
}


