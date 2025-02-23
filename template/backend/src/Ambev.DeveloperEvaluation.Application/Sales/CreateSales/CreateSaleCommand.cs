using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        /// <summary>
        /// Nome do produto associado à venda.
        /// </summary>
        public string Productname { get; set; }

        /// <summary>
        /// Preço unitário do produto.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Desconto da venda.
        /// </summary>
        public int Discount { get; set; } = 0;

        /// <summary>
        /// Lista de itens da venda, cada item possui quantidade e preço unitário.
        /// </summary>
        public List<SaleItem> Items { get; set; }

        /// <summary>
        /// Inicializa a classe <see cref="CreateSaleCommand"/>.
        /// </summary>
        public CreateSaleCommand()
        {
            Items = new List<SaleItem>();
        }
    }
        
}
