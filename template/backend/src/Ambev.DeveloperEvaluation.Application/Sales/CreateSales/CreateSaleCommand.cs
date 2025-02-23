using MediatR;
using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        
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
