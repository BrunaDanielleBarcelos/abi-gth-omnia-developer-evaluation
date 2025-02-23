namespace Ambev.DeveloperEvaluation.Application.Sales;

public class SaleItem
{  
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int Discount { get; set; }
    public decimal TotalPrice { get; set; }
}

