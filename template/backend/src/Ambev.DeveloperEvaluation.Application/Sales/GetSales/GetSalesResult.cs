using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

/// <summary>
/// Response model for GetSales operation
/// </summary>
public class GetSalesResult
{
    
    /// <summary>
    /// The name of the product associated with the sale
    /// </summary>
    public string CodigoVenda { get; set; }
    
    /// <summary>
    /// The list of sale items associated with the sale
    /// </summary>
    public List<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// The timestamp when the sale was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

