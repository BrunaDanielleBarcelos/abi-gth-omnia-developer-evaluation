using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class GetSaleByIdRequest : IRequest<GetSaleByIdResponse>
    {
        public string CodigoVenda { get; set; }
    }
}
