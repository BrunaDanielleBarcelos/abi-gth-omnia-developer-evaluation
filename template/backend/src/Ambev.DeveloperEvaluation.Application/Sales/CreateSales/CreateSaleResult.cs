using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales;

public class CreateSaleResult
{
    public string? CodigoVenda { get; set; }

    public List<SaleItem> Items { get; set; }    

}

